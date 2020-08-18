using Application.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Application.Mapper
{
    public static class CustomMapper
    {
        public static CreateInteractionCommand MapToCreateInteractionCommand(HttpRequest Request, string p, string t, string s, string b, string session, string key)
        {
            return new CreateInteractionCommand()
            {
                IsBot = Int32.Parse(b),
                Key = key,
                Session = session,
                Path = p,
                Title = t,
                ScreenSizeAttr = s,
                RemoteIpAddress = Request.Headers["X-Real-IP"],
                Browser = Request.Headers["User-Agent"],
                Language = Request.Headers["Accept-Language"],
                Referrer = Request.Headers["Referer"]
            };
        }
    }
}
