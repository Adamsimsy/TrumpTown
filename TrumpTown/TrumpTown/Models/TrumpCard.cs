using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrumpTown.Models
{
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

}