using System.Collections.Generic;
using NUnit.Framework;
using Server.Data;

namespace ServerTests
{
    public class DbControllerTests
    {
        [Test]
        public void GetAllRecordsSuccess()
        {
            var db = new DbController("mongodb://localhost:40000");
            db.Database = "test";
            db.Collection = "users";

            var expected = new Person[]
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
                        {"c++", 3},
                        {"python", 5}
                    }
                }
            };

            var result = db.GetAllRecords<Person>().Result;
            
            Assert.AreEqual(expected, result);
        }
    }
}