using System;
using MongoDB.Bson;

namespace VigilantIIS.WebDashboard.Models
{
    public class HttpReq
    {
        public String _id { get; set; }
        public DateTime RequestedOn { get; set; }
        public String SiteName { get; set; }
        public String UserAgent { get; set; }
        public String Referrer { get; set; }
        public String Url { get; set; }
        public String Port { get; set; }
        public String Authority { get; set; }

        public static BsonDocument HttpReqToBsonDocument(HttpReq model)
        {
            var document = new BsonDocument()
            {
                {"RequestedOn", model.RequestedOn.ToString()},
                {"SiteName", model.SiteName},
                {"UserAgent", model.UserAgent},
                {"Referrer", model.Referrer},
                {"Url", model.Url},
                {"Port", model.Port},
                {"Authority", model.Authority}
            };

            return document;
        }

        public static HttpReq BsonDocumentToHttpReq(BsonDocument document)
        {
            return new HttpReq()
            {
                _id = document["_id"].ToString(),
                RequestedOn = DateTime.Parse(document["RequestedOn"].ToString()),
                SiteName = document["SiteName"].ToString(),
                UserAgent = document["UserAgent"].ToString(),
                Referrer = document["Referrer"].ToString(),
                Url = document["Url"].ToString(),
                Port = document["Port"].ToString(),
                Authority = document["Authority"].ToString()
            };
        }
    }
}