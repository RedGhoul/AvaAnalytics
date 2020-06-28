using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using SharpCounter.Data;
using SharpCounter.Enities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace SharpCounter.Dapper
{
    public class BrowserStatsRepo : IRepository<BrowserStats>
    {
        private readonly string connectionString;
        public BrowserStatsRepo(IConfiguration configuration)
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

        public Task Add(BrowserStats item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BrowserStatDTO>> GetBrowserStats(DateTime curTime, DateTime oldTime)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return await dbConnection.QueryAsync<BrowserStatDTO>(
                @"SELECT ""Browser"", ""Version"", SUM(""Count"") as ""Count"" FROM ""BrowserStats"" where  
                ""Date"" <= @curTime and ""Date"" >= @oldTime GROUP By ""Browser"", ""Version""",
                new { curTime, oldTime });
        }

        public Task<IEnumerable<BrowserStats>> FindAll()
        {
            throw new NotImplementedException();
        }

        public BrowserStats FindByID(int id)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(BrowserStats item)
        {
            throw new NotImplementedException();
        }
    }
}
