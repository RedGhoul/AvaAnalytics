using Dapper;
using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class WebSiteRepo
    {
        private readonly string connectionString;
        public WebSiteRepo(IConfiguration configuration)
        {
            connectionString = AppSecrets.GetConnectionString(configuration);
        }


        public async Task<ICollection<WebSites>> FindAll()
        {
            using IDbConnection db = new SqlConnection(connectionString);
            IEnumerable<WebSites> result = await db.QueryAsync<WebSites>(@"SELECT * FROM WebSites");
            return result.ToList();
        }

        public WebSites FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<WebSites> FindByAPIKey(string key)
        {
            using IDbConnection db = new SqlConnection(connectionString);
            return await db.QueryFirstOrDefaultAsync<WebSites>(
                    @"SELECT * FROM WebSites Where APIKey = @key", new { key });
        }


    }
}
