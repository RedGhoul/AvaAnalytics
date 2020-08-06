﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SharpCounter.Dapper;
using SharpCounter.Enities;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

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
        private readonly ILogger<InteractionController> _Logger;

        public InteractionController(ILogger<InteractionController> logger,
            InteractionRepo interactionRepo, SessionRepo SessionRepo,
            WebSiteRepo WebsiteRepo, IDistributedCache cache)
        {
            _cache = cache;
            _websiteRepo = WebsiteRepo;
            _sessionRepo = SessionRepo;
            _interactionRepo = interactionRepo;
            _Logger = logger;
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
                double ScreenWidth = 0;
                double ScreenHeight = 0;
                double DevicePixelRatio = 0;
                if (screenProps.Length == 3)
                {
                    ScreenWidth = double.Parse(screenProps[0]);
                    ScreenHeight = double.Parse(screenProps[1]);
                    DevicePixelRatio = double.Parse(String.Format("{0:0.##}", screenProps[2]));
                }

                string Country = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                //Country = result.Country.Name;
                //try
                //{
                //    using var reader = new DatabaseReader("GeoLite2-Country.mmdb");
                //    var result = reader.Country(Request.HttpContext
                //        .Connection.RemoteIpAddress.ToString());
                //    Country = result.Country.Name;
                //}
                //catch (Exception ex)
                //{
                //    _Logger.LogError(ex.Message);
                //    _Logger.LogError(ex.StackTrace);
                //}


                Interaction interaction = new Interaction
                {
                    WebSiteId = curSite.Id,
                    SessionId = curSession.Id,
                    Path = p,
                    Title = t,
                    Location = Country,
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
            byte[] image = await _cache.GetAsync("image");
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
            string result = default(string);

            using (SHA512Managed algo = new SHA512Managed())
            {
                result = GenerateHashString(algo, text);
            }

            return result;
        }

        private static string GenerateHashString(HashAlgorithm algo, string text)
        {
            algo.ComputeHash(Encoding.UTF8.GetBytes(text));
            byte[] result = algo.Hash;
            return string.Join(
                string.Empty,
                result.Select(x => x.ToString("x2")));
        }
    }
}