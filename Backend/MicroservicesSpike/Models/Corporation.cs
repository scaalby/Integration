using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MicroservicesSpike.Models
{
    public class Corporation
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        public string Nation { get; set; }

        public List<RealEstate> RealEstates { get; set; }
    }
}