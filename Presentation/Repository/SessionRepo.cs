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
    public class SessionRepo
    {
        private readonly ApplicationDbContext _context;

        public SessionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Session item)
        {
            await _context.Sessions.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Session> FindBySessionHash(string sessionHash)
        {
            return await _context.Sessions.Where(x => x.SessionUId == sessionHash).FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Session>> FindAll()
        {
            throw new NotImplementedException();
        }


        public Session FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

    }
}
