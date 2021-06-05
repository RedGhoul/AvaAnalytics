using Dapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class WebSiteRepo
    {
        private readonly ApplicationDbContext _context;
        public WebSiteRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<WebSites>> FindAll()
        {
            return await _context.WebSites.ToListAsync();
        }

        public async Task<WebSites> FindByAPIKey(string key)
        {
            return await _context.WebSites.Where(x => x.APIKey.Equals(key)).FirstOrDefaultAsync();
        }

        public WebSites FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(WebSites item)
        {
            throw new NotImplementedException();
        }

        public Task Add(WebSites item)
        {
            throw new NotImplementedException();
        }

    }
}
