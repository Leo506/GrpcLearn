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

            var result = db.GetAllRecords<Person>().Result;
            
            Assert.AreEqual(DataForTests.Persons, result);
        }
    }
}