using System.Collections.Generic;
using System.Threading.Tasks;
using VigilantIIS.WebDashboard.Models;

namespace VigilantIIS.WebDashboard.Repository
{
    public interface IMongoDbRepository
    {
        Task<IEnumerable<WebRequestModel>> GetAllRecords();
        WebRequestModel GetEntityById(string id);
        void Add(WebRequestModel entity);
        void Update(string objectId, WebRequestModel entity);
        void Delete(string objectId);         
    }
}