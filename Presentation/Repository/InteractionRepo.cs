using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;
using Persistence;
using System.Data;
using System.Data.SqlClient;
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
            await _context.Interactions.AddAsync(item);
            await _context.SaveChangesAsync();
        }
    }
}
