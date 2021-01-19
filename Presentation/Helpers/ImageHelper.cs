using Microsoft.Extensions.Caching.Distributed;
using Presentation.Repository;
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
        public static (MemoryStream, string) SendPngStream(CacheRepo _cache)
        {
            byte[] image = Encoding.ASCII.GetBytes(_cache.GetStringAsync("image"));
            if (image == null)
            {
                MemoryStream memStreams = new MemoryStream();
                memStreams.Position = 0;
                image = memStreams.ToArray();
                _cache.SetStringAsync("image", image.ToString());
            }
            return (new MemoryStream(), "image/png");
        }


    }
}
