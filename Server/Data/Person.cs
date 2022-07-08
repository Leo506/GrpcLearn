using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Server.Data
{
    public class Person
    {
        [BsonIgnoreIfNull]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Class { get; set; }
        public Dictionary<string, int> Marks { get; set; }

        public override bool Equals(object obj)
        {
            var person = obj as Person;
            if (person == null)
                return false;

            bool result =
                Name == person.Name &&
                Age == person.Age &&
                Class == person.Class &&
                Marks.Count == person.Marks.Count;

            foreach (var mark in Marks)
            {
                if (!person.Marks.ContainsKey(mark.Key))
                    result = false;
            }

            return result;
        }
    }
}