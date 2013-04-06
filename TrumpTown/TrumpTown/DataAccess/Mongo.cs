using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace TrumpTown.DataAccess
{
    public class MongoData
    {
        public const string ConnectionString = "mongodb://MongoLab-2:OodroYtw5YXihgJPNlMlH79xc9KmMQ5nswjedkk73Xo-@ds045087.mongolab.com:45087/MongoLab-2";
        public const string DatabaseName = "MongoLab-2";
        //public string WhichDatabase { get { return "Mongo"; } }

        public MongoClient ClientConnection
        {
            get { return Connect(); }
        }

        private MongoClient Connect()
        {
            return new MongoClient(ConnectionString);
        }

        private MongoDatabase GetDatabase()
        {
            var server = ClientConnection.GetServer();
            return server.GetDatabase(DatabaseName);
        }


        public long GetCount()
        {
            var db = GetDatabase();
            return db.GetCollection("areas").Count();
        }


    

        public BsonDocument GetRecord(string id)
        {
            var db = GetDatabase();
            var query = Query.LTE("_id", new ObjectId(id));

            return db.GetCollection("areas").Find(query).FirstOrDefault();

        }

        public IEnumerable<BsonDocument> GetByIds(IEnumerable<BsonValue> ids)
        {
            var db = GetDatabase();
            var query = Query.In("_id", ids);
            return db.GetCollection("areas").Find(query);
        }

    }
}
