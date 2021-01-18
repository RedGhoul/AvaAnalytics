using Dapper;
using Domain;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Application.Repository
{
    public class SessionRepo
    {
        private readonly string connectionString;
        public SessionRepo(IConfiguration configuration)
        {
            connectionString = AppSecrets.GetConnectionString(configuration);
        }

        internal IDbConnection Connection => new MySqlConnection(connectionString);

        public async Task Add(Session item)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            await dbConnection.ExecuteAsync(
                @"Insert into ""Sessions""
                (""SessionUId"", ""LastSeen"", ""CreatedAt"", ""WebSiteId"") VALUES
                (@SessionUId, @LastSeen, @CreatedAt, @WebSiteId)", item);
        }
        public async Task<Session> FindBySessionHash(string sessionHash)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            return await dbConnection.QueryFirstOrDefaultAsync<Session>(
                    @"SELECT * FROM ""Sessions"" where ""SessionUId"" = @sessionHash",
                     new { sessionHash });

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
