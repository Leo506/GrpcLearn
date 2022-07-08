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
        private Person[] _persons;

        [SetUp]
        public void Setup()
        {
            _persons = new[]
            {
                new Person()
                {
                    Name = "Tommi",
                    Age = 20,
                    Class = "2b",
                    Marks = new Dictionary<string, int>()
                    {
                        { "c++", 5 },
                        { "python", 4 }
                    }
                },
                new Person()
                {
                    Name = "Kate",
                    Age = 20,
                    Class = "2b",
                    Marks = new Dictionary<string, int>()
                    {
                        { "c++", 3 },
                        { "python", 5 }
                    }
                }
            };
        }
        
        
        
        [Test]
        public void GetAllUsersSuccess()
        {
            var service = new DbService(new TestDbWorker(_persons));

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
            
            Assert.AreEqual(_persons, result.ToArray());
        }
    }
}

class TestDbWorker : IDbWorker
{
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