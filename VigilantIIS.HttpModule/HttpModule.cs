using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using MongoDB.Driver;

namespace VigilantIIS.HttpModule
{
    public class HttpModule : IHttpModule
    {
        private readonly IMongoClient _mongoClient;
        private IMongoCollection<HttpReq> _httpReqs;
        private const string ConnectionString = "mongodb://localhost/?safe=true";

        public HttpModule()
            : this(new MongoClient(ConnectionString))
        {
        }

        public HttpModule(IMongoClient client)
        {
            _mongoClient = client;
        }

        public void Init(HttpApplication context)
        {
            // check if mongo is running

            var db = _mongoClient.GetDatabase("dashboard");

            _httpReqs = db.GetCollection<HttpReq>("requests");

            context.BeginRequest += (sender, e) => LogRequest(context);

        }

        private void LogRequest(HttpApplication context)
        {
            _httpReqs.InsertOneAsync(new HttpReq(context.Request));
        }

        public void Dispose()
        {

        }
    }
}
