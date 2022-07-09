using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using NUnit.Framework;
using Server;
using Server.Data;
using Server.Services;
using DbService = Server.Services.DbService;

namespace ServerTests
{
    public class DbServiceTests
    {
        [Test]
        public void GetAllUsersSuccess()
        {
            var service = new DbService(new TestDbWorker(DataForTests.Persons));

            Console.WriteLine(default(Person));
            var reply = service.GetAllUsers(new EmptyUserRequest(), null).Result;

            var result = new List<Person>();
            foreach (var userReply in reply.Users)
            {
                result.Add(new Person()
                {
                    Name = userReply.Name,
                    Age = userReply.Age,
                    Class = userReply.Class,
                    Marks = userReply.Marks.ToDictionary(m => m.Key, m => m.Value)
                });
            }
            
            Assert.AreEqual(DataForTests.Persons, result.ToArray());
        }


        [Test]
        public void AddNewRecordSuccess()
        {
            var service = new DbService(new TestDbWorker(DataForTests.Persons));
            var reply = service.AddNewUser(new User(), null).Result;
            
            Assert.AreEqual(StatusReply.Types.Status.Ok, reply.Status);
        }
    }
}

class TestDbWorker : IDbWorker
{
    public Task<bool> AddNewRecord<T>(T itemToAdd)
    {
        return Task.FromResult(true);
    }

    public string Database { get; set; }
    public string Collection { get; set; }
 
    private readonly Person[] _persons;

    public TestDbWorker(Person[] persons)
    {
        _persons = persons;
    }
    public Task<T[]> GetAllRecords<T>()
    {
        T[] toReturn = new T[_persons.Length];
        for (int i = 0; i < _persons.Length; i++)
        {
            toReturn[i] = (T)Convert.ChangeType(_persons[i], typeof(T));
        }

        return Task.FromResult(toReturn);
    }

}