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
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.GridFS;
using MongoDB.Driver.Linq;

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

        public int GetCount()
        {
            var db = GetDatabase();
            return (int)db.GetCollection("areas").Count();
        }




        public string GetRecord()
        {
            var db = GetDatabase();
            var collection = db.GetCollection("areas");
            try
            {

           //hardcode the count in for now as we are running out of time...
            //var count = GetCount();
                var count = 50000;

            var randomNumber = new Random();

            if (count <= 0)
                count = 2;

            int randomInt = randomNumber.Next(0, count - 1); 
           

            var card = collection.AsQueryable()
                .Skip(randomInt)
                .Take(1).FirstOrDefault().ToJson();

                return card ?? collection.FindOne().ToJson();

            }
            catch (Exception ex)
            {
                //swallow expection, it's a hack day after all...
                return collection.FindOne().ToJson();
            }
   

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