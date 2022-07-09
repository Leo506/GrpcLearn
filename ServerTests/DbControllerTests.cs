using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using NUnit.Framework;
using Server.Data;

namespace ServerTests
{
    public class DbControllerTests
    {
        [Test]
        public void GetAllRecordsSuccess()
        {
            var db = CreateDbController();

            var result = db.GetAllRecords<Person>().Result;

            foreach (var person in DataForTests.Persons)
            {
                Assert.IsTrue(result.ToList().Contains(person));
            }
        }


        [Test]
        public void AddNewRecordSuccess()
        {
            var db = CreateDbController();

            var personToAdd = new Person()
            {
                Name = "Artem",
                Age = 18,
                Class = "po",
                Marks = new Dictionary<string, int>()
                {
                    { "c++", 5 },
                    { "python", 5 }
                }
            };

            var result = db.AddNewRecord<Person>(personToAdd).Result;

            var allUsers = db.GetAllRecords<Person>().Result;
            
            Assert.IsTrue(result);
            Assert.IsTrue(allUsers.ToList().Contains(personToAdd));
        }


        private DbController CreateDbController()
        {
            var db = new DbController("mongodb://localhost:40000");
            db.Database = "test";
            db.Collection = "users";

            return db;
        }
    }
}