﻿using Application.Commands;
using Application.Helpers;
using Application.Repository;
using Application.Response;
using Domain;
using MaxMind.GeoIP2;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Repository;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Handlers
{
    public class CreateInteractionHandler : IRequestHandler<CreateInteractionCommand, CreateInteractionResponse>
    {
        private readonly CacheRepo _cache;
        private readonly WebSiteRepo _websiteRepo;
        private readonly SessionRepo _sessionRepo;
        private readonly InteractionRepo _interactionRepo;
        private readonly ILogger<CreateInteractionHandler> _Logger;

        public CreateInteractionHandler(ILogger<CreateInteractionHandler> logger,
           InteractionRepo interactionRepo, SessionRepo SessionRepo,
           WebSiteRepo WebsiteRepo, CacheRepo CacheRepo)
        {
            _websiteRepo = WebsiteRepo;
            _sessionRepo = SessionRepo;
            _interactionRepo = interactionRepo;
            _Logger = logger;
            _cache = CacheRepo;
        }

        public async Task<CreateInteractionResponse> Handle(CreateInteractionCommand request, CancellationToken cancellationToken)
        {
            if (request.IsBot == 0)
            {
                string sessionHash = CryptoHelper.SHA512(request.Session);

                WebSites curSite = null;
                Session curSession = null;

                string site = _cache.GetStringAsync(request.Key);
                if (site != null)
                {
                    curSite = JsonConvert.DeserializeObject<WebSites>(site);
                }
                if (curSite == null)
                {
                    curSite = await _websiteRepo.FindByAPIKey(request.Key);
                    if (curSite == null)
                    {
                        return await SendResponse();
                    }
                    _cache.SetStringAsync(request.Key, JsonConvert.SerializeObject(curSite));
                }
                bool isFirst = false;
                string sessionString = _cache.GetStringAsync(sessionHash);
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
                    _cache.SetStringAsync(sessionHash,
                        JsonConvert.SerializeObject(curSession,
                        Formatting.Indented, new JsonSerializerSettings
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));
                }

                string[] screenProps = request.ScreenSizeAttr.Split(",");
                double ScreenWidth = 0;
                double ScreenHeight = 0;
                double DevicePixelRatio = 0;
                if (screenProps.Length == 3)
                {
                    ScreenWidth = double.Parse(screenProps[0]);
                    ScreenHeight = double.Parse(screenProps[1]);
                    DevicePixelRatio = double.Parse(String.Format("{0:0.##}", screenProps[2]));
                }

                string Country = request.RemoteIpAddress;

                try
                {
                    using var reader = new DatabaseReader("GeoLite2-Country.mmdb");
                    var result = reader.Country(request.RemoteIpAddress ?? "");
                    Country = result.Country.Name;
                }
                catch (Exception ex)
                {
                    _Logger.LogError(ex.Message);
                    _Logger.LogError(ex.StackTrace);
                }


                Interaction interaction = new Interaction
                {
                    WebSiteId = curSite.Id,
                    SessionId = curSession.Id,
                    Path = request.Path,
                    Title = request.Title,
                    Location = Country,
                    Browser = request.Browser,
                    Language = request.Language,
                    FirstVisit = isFirst,
                    Referrer = request.Referrer,
                    ScreenWidth = ScreenWidth,
                    ScreenHeight = ScreenHeight,
                    DevicePixelRatio = DevicePixelRatio,
                    CreatedAt = DateTime.UtcNow
                };

                await _interactionRepo.Add(interaction);
            }
            return await SendResponse();
        }

        private Task<CreateInteractionResponse> SendResponse()
        {
            var ResponseInfo = ImageHelper.SendPngStream(_cache);

            return Task.FromResult(new CreateInteractionResponse()
            {
                ContentType = ResponseInfo.Item2,
                Image = ResponseInfo.Item1
            });
        }
    }
}
