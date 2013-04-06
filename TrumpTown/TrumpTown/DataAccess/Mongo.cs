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
using TrumpTown.Models;

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




        public TrumpCard GetRecord(string id)
        {
            Stopwatch s = new Stopwatch();
            s.Start();
            var db = GetDatabase();
            //var query = Query.LTE("_id", );

            return db.GetCollection<TrumpCard>("areas").FindOneById(new ObjectId(id));

        }

        public IEnumerable<TrumpCard> GetByIds(IEnumerable<BsonValue> ids)
        {
            var db = GetDatabase();
            var query = Query.In("_id", ids);
            return db.GetCollection<TrumpCard>("areas").Find(query);
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