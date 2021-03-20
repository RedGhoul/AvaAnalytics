using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Persistence;
using System.Data;

using System.Threading.Tasks;

namespace Application.Repository
{
    public class InteractionRepo
    {
        private readonly ApplicationDbContext _context;

        public InteractionRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task Add(Interaction item)
        {
            _context.Interactions.Add(item);
            await _context.SaveChangesAsync();
        }
    }
}
