using System.Collections.Generic;
using Server.Data;

namespace ServerTests
{
    public static class DataForTests
    {
        public static Person[] Persons = new[]
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
}