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
    public class InteractionController : ControllerBase
    {
        private readonly IDistributedCache _cache;
        private readonly WebSiteRepo _websiteRepo;
        private readonly SessionRepo _sessionRepo;
        private readonly InteractionRepo _interactionRepo;

        public InteractionController(InteractionRepo interactionRepo, SessionRepo SessionRepo, WebSiteRepo WebsiteRepo, IDistributedCache cache)
        {
            _cache = cache;
            _websiteRepo = WebsiteRepo;
            _sessionRepo = SessionRepo;
            _interactionRepo = interactionRepo;
        }


        [HttpGet("Count")]
        public async Task<ActionResult> AddInteraction(string p, string r, string t, string s, string b, string session, string key)
        {

            if (Int32.Parse(b) == 0)
            {
                string sessionHash = SHA512(session);

                WebSites curSite = null;
                Session curSession = null;

                string site = await _cache.GetStringAsync(key);
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
                        curSession = await _sessionRepo.FindBySessionHash(sessionHash);
                        isFirst = true;
                    }
                    await _cache.SetStringAsync(sessionHash,
                        JsonConvert.SerializeObject(curSession,
                        Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                }

                string[] screenProps = s.Split(",");
                float ScreenWidth = 0;
                float ScreenHeight = 0;
                float DevicePixelRatio = 0;
                if (screenProps.Length == 3)
                {
                    ScreenWidth = float.Parse(screenProps[0]);
                    ScreenHeight = float.Parse(screenProps[1]);
                    DevicePixelRatio = float.Parse(screenProps[2]);
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
                    Referrer = Request.Headers["Referer"].ToString(),
                    ScreenWidth = ScreenWidth,
                    ScreenHeight = ScreenHeight,
                    DevicePixelRatio = DevicePixelRatio,
                    CreatedAt = DateTime.UtcNow
                };
               
                await _interactionRepo.Add(interaction);
            }

            return await SendPngAsync();
        }

        private async Task<ActionResult> SendPngAsync()
        {
            var image = await _cache.GetAsync("image");
            if (image == null)
            {
                Bitmap icon = new Bitmap(1, 1);
                MemoryStream memStreams = new MemoryStream();
                icon.Save(memStreams, ImageFormat.Png);
                memStreams.Position = 0;
                image = memStreams.ToArray();
                await _cache.SetAsync("image", image);
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
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));
            var result = algo.Hash;
            return string.Join(
                string.Empty,
                result.Select(x => x.ToString("x2")));
        }
    }
}
