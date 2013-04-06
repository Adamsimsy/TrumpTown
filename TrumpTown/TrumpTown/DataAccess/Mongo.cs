using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrumpTown.DataAccess
{
    public class MongoData
    {
        //public const string ConnectionString = "mongodb://localhost";
        //public const string DatabaseName = "test";
        //public string WhichDatabase { get { return "Mongo"; } }

        //public MongoClient ClientConnection
        //{
        //    get { return Connect(); }
        //}

        //private MongoClient Connect()
        //{
        //    return new MongoClient(ConnectionString);
        //}

        //private MongoDatabase GetDatabase()
        //{
        //    var server = ClientConnection.GetServer();
        //    return server.GetDatabase(DatabaseName);
        //}

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

        //public long GetCount()
        //{
        //    var db = GetDatabase();
        //    return db.GetCollection<MongoEntity>("entities").Count();
        //}

        //public IEnumerable<BaseEntity> GetLatestRecords(int numberRecords, out long timeElapsed)
        //{
        //    Stopwatch s = new Stopwatch();
        //    List<BaseEntity> baseEntities = new List<BaseEntity>();
        //    s.Start();
        //    var db = GetDatabase();
        //    //var query = Query.LTE("InsertDate", DateTime.Now.ToUniversalTime());
        //    var collection = db.GetCollection<MongoEntity>("entities");

        //    var result = collection.AsQueryable<MongoEntity>()
        //        .OrderByDescending(x => x.InsertDate)
        //        .ThenBy(x => x.Name)
        //        .ThenBy(x => x.Iteration)
        //        .Take(numberRecords).ToList();

        //    s.Stop();
        //    timeElapsed = s.ElapsedMilliseconds;

        //    foreach (var item in result)
        //    {
        //        BaseEntity mongoAsBase = new BaseEntity
        //        {
        //            Id = item.Id.ToString(),
        //            Name = item.Name,
        //            Iteration = (int)item.Iteration,
        //            InsertDate = DateTime.Parse(item.InsertDate.ToString())
        //        };
        //        baseEntities.Add(mongoAsBase);
        //    }

        //    return baseEntities;


        //}

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