using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace Server.Services
{
    public class DbService : Server.DbService.DbServiceBase
    {
        private readonly ILogger<DbService> _logger;

        public DbService(ILogger<DbService> logger)
        {
            _logger = logger;
        }
        
        public DbService() {}


        public override Task<UserReply> GetUserByName(UserRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserReply()
            {
                Name = "Test",
                Age = 20,
                Class = "2b",
                Marks = { { "c++", 20 } }
            });
        }
    }
}