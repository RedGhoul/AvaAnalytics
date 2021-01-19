using System;
using System.Linq;
using Persistence;

namespace Presentation.Repository
{
    public class CacheRepo
    {
        private readonly ApplicationDbContext _context;

        public CacheRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public string GetStringAsync(string key)
        {
            var value = _context.Caches.Where(x => x.Key.Equals(key)).FirstOrDefault();
            if(value != null)
            {
                return value.Value;
            }
            return "";
        }

        public void SetStringAsync(string key, string value)
        {
            var newCaches = new Cache() {
                Key = key,
                Value = value
            };

            _context.Caches.Add(newCaches);
            _context.SaveChanges();

        }

    }

}
