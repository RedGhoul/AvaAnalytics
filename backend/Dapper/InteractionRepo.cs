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
    public class InteractionRepo
    {
        private readonly string connectionString;
        public InteractionRepo(IConfiguration configuration)
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

        public async Task Add(Interaction item)
        {
            using IDbConnection dbConnection = Connection;
            dbConnection.Open();
            await dbConnection.ExecuteAsync(
                 @"Insert into ""Interactions""
                (""WebSiteId"", ""SessionId"", ""Path"", ""Title"",""Browser"",
                 ""Location"", ""Language"", ""FirstVisit"", ""Referrer"", ""CreatedAt"",
                 ""ScreenWidth"", ""ScreenHeight"",""DevicePixelRatio"") 
                VALUES
                (@WebSiteId, @SessionId, @Path, @Title, @Browser, @Location,
                 @Language, @FirstVisit, @Referrer, @CreatedAt, @ScreenWidth,
                 @ScreenHeight,@DevicePixelRatio)", item);
        }
    }
}
