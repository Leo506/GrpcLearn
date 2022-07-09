using System.Linq;

namespace Server.Data
{
    public static class UserExtension
    {
        public static Person ToPerson(this User user)
        {
            return new Person()
            {
                Name = user.Name,
                Age = user.Age,
                Class = user.Class,
                Marks = user.Marks.ToDictionary(m => m.Key, m => m.Value)
            };
        }
    }
}