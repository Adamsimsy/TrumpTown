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

        //public long InsertRecords(string name, int iterations)
        //{
        //    Stopwatch s = Stopwatch.StartNew();
        //    var db = GetDatabase();
        //    var collection = db.GetCollection<MongoEntity>("entities");

        //    for (var i = 1; i <= iterations; i++)
        //    {
        //        var entity = new MongoEntity { Name = name, Iteration = i, InsertDate = DateTime.Now };
        //        collection.Insert(entity);
        //    }
        //    s.Stop();

        //    return s.ElapsedMilliseconds;
        //}

        public long GetCount()
        {
            var db = GetDatabase();
            return db.GetCollection("areas").Count();
        }


        public class TrumpCard
        {
            [BsonElement("CTY_CODE")]
            public string CityCode { get; set; }

            [BsonElement("CTY_NAME")]
            public string CityName { get; set; }

            [BsonElement("_id")]
            public ObjectId Id { get; set; }

            [BsonElement("AREA_METADATA")]
            public string AreaMetaData { get; set; }

            [BsonElement("All Deaths")]
            public int AllDeaths { get; set; }

            [BsonElement("Females; Cancer")]
            public string FemalesDeathCancer { get; set; }

            [BsonElement("Females; All Causes")]
            public string FemalesAllDeaths { get; set; }

            [BsonElement("Females; Cerebrovascular Disease (including Stroke)")]
            public string FemalesDeathCerebro { get; set; }

            [BsonElement("Females; Coronary Heart Disease")]
            public string FemalesDeathHeart { get; set; }

            [BsonElement("GOR_CODE")]
            public string GorCode { get; set; }


            [BsonElement("GOR_NAME")]
            public string GorName { get; set; }

            [BsonElement("LA_CODE")]
            public string LaCode { get; set; }

            [BsonElement("Males; All Causes")]
            public string MalesDeathAll { get; set; }

            [BsonElement("Males; Cancer")]
            public string MalesDeathCancer { get; set; }

            [BsonElement("Males; Cerebrovascular Disease (including Stroke)")]
            public string MalesDeathCerebro { get; set; }

            [BsonElement("Males; Coronary Heart Disease")]
            public string MalesDeathHeart { get; set; }

            [BsonElement("H1")]
            public string H1 { get; set; }

            [BsonElement("LA_NAME")]
            public string LAName { get; set; }

        }


        public TrumpCard GetRecord(string id)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            var db = GetDatabase();
            var query = Query.LTE("_id", new ObjectId(id));

            return db.GetCollection<TrumpCard>("areas").Find(query).FirstOrDefault();

        }

        //public long DeleteRecord(string name, out long documentsAffected)
        //{
        //    Stopwatch s = Stopwatch.StartNew();

        //    var db = GetDatabase();
        //    var collection = db.GetCollection<MongoEntity>("entities");
        //    var query = Query.EQ("Name", name);
        //    var a = collection.Remove(query);

        //    s.Stop();

        //    documentsAffected = a.DocumentsAffected;

        //    return s.ElapsedMilliseconds;

        //}
    }
}