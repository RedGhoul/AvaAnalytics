using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;
using Npgsql;
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

        internal IDbConnection Connection => new NpgsqlConnection(connectionString);


        public async Task<ICollection<WebSites>> FindAll()
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            IEnumerable<WebSites> result = await dbConnection.QueryAsync<WebSites>(@"SELECT * FROM ""WebSites""");
            return result.ToList();
        }

        public WebSites FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<WebSites> FindByAPIKey(string key)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<WebSites>(
                    @"SELECT * FROM ""WebSites"" Where ""APIKey"" = @key",
                    new { key });
        }


    }
}
