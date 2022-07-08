using System.Linq;
using System.Threading.Tasks;
using Google.Protobuf.Collections;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using Server.Data;
using System.Collections.Generic;

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

        public override Task<UsersArrayReply> GetAllUsers(EmptyUserRequest request, ServerCallContext context)
        {
            var db = new DbController("mongodb://localhost:40000");
            db.Database = request.Database;
            db.Collection = request.Collection;

            var result = db.GetAllRecords<Person>().Result;
            var usersList = new List<UserReply>();
            foreach (var person in result)
            {
                var toAdd = new UserReply()
                {
                    Name = person.Name,
                    Age = person.Age,
                    Class = person.Class
                };
                
                toAdd.Marks.Add(person.Marks);
                
                usersList.Add(toAdd);
            }

            var respones = new UsersArrayReply();
            respones.Users.Add(usersList);

            return Task.FromResult(respones);
        }
    }
}