using Dapper;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class SessionRepo
    {
        private readonly ApplicationDbContext _context;

        public SessionRepo(ApplicationDbContext context,IConfiguration configuration)
        {
            _context = context;
        }

        public async Task Add(Session item)
        {
            _context.Sessions.Add(item);
            await _context.SaveChangesAsync();
        }
        public async Task<Session> FindBySessionHash(string sessionHash)
        {
            var Session = await _context.Sessions.Where(x => x.SessionUId.Equals(sessionHash)).FirstOrDefaultAsync();
            return Session;
        }

    }
}
