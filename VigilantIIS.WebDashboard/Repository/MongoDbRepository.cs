using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using VigilantIIS.WebDashboard.Models;

namespace VigilantIIS.WebDashboard.Repository
{
    public class MongoDbRepository : IMongoDbRepository
    {

        public IMongoDatabase Database;
        public IMongoCollection<BsonDocument> EntityCollection;
        public HttpReq Entites = new HttpReq();
        public MongoClient Client;

        // Constructor
        public MongoDbRepository()
        {
            // Get the Mongo Client
            Client = new MongoClient();

            // Assign the database to mongoDatabase
            Database = Client.GetDatabase(ConfigurationManager.AppSettings["MongoDBName"]);

            // get the Employees collection (table) and assign to EmployeesCollection
            EntityCollection = Database.GetCollection<BsonDocument>("requests");
        }

        /// <summary>
        /// Currently only grabs 100 records
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<HttpReq>> GetAllRecords()
        {
            var entities = new List<HttpReq>();
            var collection = Database.GetCollection<BsonDocument>("requests");
            var filter = new BsonDocument();

            using (var cursor = await collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    entities.AddRange(batch.Select(HttpReq.BsonDocumentToHttpReq).Take(100));
                }
            }

            return entities;
        }

        public HttpReq GetEntityById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                throw new ArgumentNullException("id", "Web Request Id is empty!");
            }

            var filter = Builders<BsonDocument>.Filter.Eq("_id", id);
            var threadResults = EntityCollection.Find(filter).ToListAsync().Result;

            var resultsFromQuery = threadResults.FirstOrDefault();

            return resultsFromQuery == null ? new HttpReq() : HttpReq.BsonDocumentToHttpReq(resultsFromQuery);
        }

        public void Add(HttpReq entity)
        {
            if (!string.IsNullOrEmpty(entity._id.ToString()))
            {
                return;
            }

            EntityCollection.InsertOneAsync(HttpReq.HttpReqToBsonDocument(entity));
        }


        public void Update(string objectId, HttpReq employee)
        {
            var updateBuilder = Builders<BsonDocument>.Update
                .Set("RequestedOn", employee.RequestedOn.ToString(CultureInfo.InvariantCulture))
                .Set("SiteName", employee.SiteName)
                .Set("UserAgent", employee.UserAgent)
                .Set("Referrer", employee.Referrer)
                .Set("Url", employee.Url)
                .Set("Port", employee.Port)
                .Set("Authority", employee.Authority);

            EntityCollection.UpdateOneAsync(Builders<BsonDocument>.Filter.Eq("_id", objectId), updateBuilder);
        }

        public void Delete(string objectId)
        {
            EntityCollection.DeleteOneAsync(Builders<BsonDocument>.Filter.Eq("_id", objectId));
        }
    }
}