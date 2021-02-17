using Presentation.Repository;
using System.IO;
using System.Text;

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
