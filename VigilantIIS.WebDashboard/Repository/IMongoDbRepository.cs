using System.Collections.Generic;
using System.Threading.Tasks;
using VigilantIIS.WebDashboard.Models;

namespace VigilantIIS.WebDashboard.Repository
{
    public interface IMongoDbRepository
    {
        Task<IEnumerable<HttpReq>> GetAllRecords();
        HttpReq GetEntityById(string id);
        void Add(HttpReq entity);
        void Update(string objectId, HttpReq entity);
        void Delete(string objectId);         
    }
}