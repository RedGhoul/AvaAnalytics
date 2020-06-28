using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using Npgsql;
using SharpCounter.Dapper;
using SharpCounter.Data;
using SharpCounter.Enities;

namespace SharpCounter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionsAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        private readonly WebSiteRepo _websiteRepo;
        private readonly SessionRepo _sessionRepo;
        private readonly InteractionRepo _interactionRepo;

        public InteractionsAPIController(InteractionRepo interactionRepo, SessionRepo SessionRepo,WebSiteRepo WebsiteRepo,IDistributedCache cache, ApplicationDbContext context)
        {
            _context = context;
            _cache = cache;
            _websiteRepo = WebsiteRepo;
            _sessionRepo = SessionRepo;
            _interactionRepo = interactionRepo;
        }


        [HttpGet("Count")]
        public async Task<ActionResult> AddInteraction(string p,string r, string t, string s, string b, string session, string key)
        {
            
            if (Int32.Parse(b) == 0)
            {
                string sessionHash = SHA512(session);
                
                WebSites curSite = null;
                Session curSession = null;

                string site = await _cache.GetStringAsync(key);
                //await _cache.RemoveAsync(key);
                if (site != null)
                {
                    curSite = JsonConvert.DeserializeObject<WebSites>(site);
                }
                if (curSite == null)
                {
                    curSite = await _websiteRepo.FindByAPIKey(key);
                    if (curSite == null)
                    {
                        return await SendPngAsync();
                    }
                    await _cache.SetStringAsync(key, JsonConvert.SerializeObject(curSite));
                }
                bool isFirst = false;
                string sessionString = await _cache.GetStringAsync(sessionHash);
                //await _cache.RemoveAsync(sessionHash);
                if (sessionString != null)
                {
                    curSession = JsonConvert.DeserializeObject<Session>(sessionString);
                }
                if (curSession == null)
                {
                    curSession = await _sessionRepo.FindBySessionHash(sessionHash);
                    if (curSession == null)
                    {
                        curSession = new Session()
                        {
                            SessionUId = sessionHash,
                            LastSeen = DateTime.UtcNow,
                            CreatedAt = DateTime.UtcNow,
                            WebSiteId = curSite.Id
                        };
                        await _sessionRepo.Add(curSession);
                        isFirst = true;
                    }
                    await _cache.SetStringAsync(sessionHash,
                        JsonConvert.SerializeObject(curSession,
                        Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                }

                Interaction interaction = new Interaction
                {
                    WebSiteId = curSite.Id,
                    SessionId = curSession.Id,
                    Path = p,
                    Title = t,
                    Location = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
                    Browser = Request.Headers["User-Agent"],
                    Language = Request.Headers["Accept-Language"],
                    FirstVisit = isFirst,
                    Referrer = r,
                    CreatedAt = DateTime.UtcNow
                };
                await _interactionRepo.Add(interaction);
               
            }

            return await SendPngAsync();
        }

        private async Task<ActionResult> SendPngAsync()
        {

            var image = await _cache.GetAsync("image");
            if(image == null)
            {
                Bitmap icon = new Bitmap(1, 1);
                MemoryStream memStreams = new MemoryStream();
                icon.Save(memStreams, ImageFormat.Png);
                memStreams.Position = 0;
                await _cache.SetAsync("image", memStreams.ToArray());
            }
            return File(new MemoryStream(image), "image/png");
        }

        public static string SHA512(string text)
        {
            var result = default(string);

            using (var algo = new SHA512Managed())
            {
                result = GenerateHashString(algo, text);
            }

            return result;
        }

        private static string GenerateHashString(HashAlgorithm algo, string text)
        {
            // Compute hash from text parameter
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));

            // Get has value in array of bytes
            var result = algo.Hash;

            // Return as hexadecimal string
            return string.Join(
                string.Empty,
                result.Select(x => x.ToString("x2")));
        }
    }
}
