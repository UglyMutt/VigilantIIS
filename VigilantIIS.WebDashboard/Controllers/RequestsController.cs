using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using VigilantIIS.WebDashboard.Models;
using VigilantIIS.WebDashboard.Repository;

namespace VigilantIIS.WebDashboard.Controllers
{
    public class RequestsController : ApiController
    {
        private readonly IMongoDbRepository _repository;

        public RequestsController() : this(new MongoDbRepository()){ }
        public RequestsController(IMongoDbRepository repository)
        {
            _repository = repository;
        }

        // GET api/Requests
        public async Task<IEnumerable<HttpReq>> Get()
        {
            var list = await _repository.GetAllRecords();
            return list.OrderByDescending(r => r.RequestedOn);

        }

        // GET api/Requests/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(string id)
        {
            _repository.Delete(id);
        }
    }
}