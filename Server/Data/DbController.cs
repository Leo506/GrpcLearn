using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Server.Data
{
    public class DbController
    {
        public string Database { get; set; }
        public string Collection { get; set; }
        
        
        private readonly string _connectionString;

        public DbController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<T[]> GetAllRecords<T>()
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase(Database);
            var collection = database.GetCollection<T>(Collection);

            var countOfDocuments = await collection.CountDocumentsAsync(new BsonDocument());
            var result = new T[countOfDocuments];

            int i = 0;
            using var cursor = await collection.FindAsync(new BsonDocument());
            while (await cursor.MoveNextAsync())
            {
                foreach (var doc in cursor.Current)
                {
                    result[i] = doc;
                    i++;
                }
            }

            return result;
        }
    }
}