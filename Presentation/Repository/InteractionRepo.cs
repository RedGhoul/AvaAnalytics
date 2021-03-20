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
        private readonly string connectionString;
        public InteractionRepo(IConfiguration configuration)
        {
            connectionString = AppSecrets.GetConnectionString(configuration);
        }

        internal IDbConnection Connection => new NpgsqlConnection(connectionString);

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
