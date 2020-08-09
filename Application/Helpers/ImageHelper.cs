using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Application.Helpers
{
    public static class ImageHelper
    {
        public static async Task<(MemoryStream, string)> SendPngStream(IDistributedCache _cache)
        {
            byte[] image = await _cache.GetAsync("image");
            if (image == null)
            {
                MemoryStream memStreams = new MemoryStream();
                memStreams.Position = 0;
                image = memStreams.ToArray();
                await _cache.SetAsync("image", image);
            }
            return (new MemoryStream(), "image/png");
        }
    }
}
