using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Server.Data
{
    public class DbController : IDbWorker
    {
        public string Database { get; set; }
        public string Collection { get; set; }
        
        
        private readonly string _connectionString;

        [ActivatorUtilitiesConstructor]
        public DbController(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<T[]> GetAllRecords<T>()
        {
            var collection = GetCollection<T>();

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

        public async Task<bool> AddNewRecord<T>(T itemToAdd)
        {
            var collection = GetCollection<T>();

            await collection.InsertOneAsync(itemToAdd);

            return true;
        }

        private IMongoCollection<T> GetCollection<T>()
        {
            var client = new MongoClient(_connectionString);
            var database = client.GetDatabase(Database);
            return database.GetCollection<T>(Collection);
        }
    }
}