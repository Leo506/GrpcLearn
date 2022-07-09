using System.Threading.Tasks;
using MongoDB.Bson.Serialization.IdGenerators;

namespace Server.Data
{
    public interface IDbWorker
    {
        string Database { get; set; }
        string Collection { get; set; }
        Task<T[]> GetAllRecords<T>();
        Task<bool> AddNewRecord<T>(T itemToAdd);
    }
}