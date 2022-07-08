using System.Threading.Tasks;

namespace Server.Data
{
    public interface IDbWorker
    {
        Task<T[]> GetAllRecords<T>();
    }
}