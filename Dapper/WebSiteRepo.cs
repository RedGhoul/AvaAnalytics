using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SharpCounter.Enities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Dapper
{
    public class WebSiteRepo
    {
        private readonly string connectionString;
        public WebSiteRepo(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }


        public async Task<ICollection<WebSites>> FindAll()
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            var result = await dbConnection.QueryAsync<WebSites>(@"SELECT * FROM ""WebSites""");
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
