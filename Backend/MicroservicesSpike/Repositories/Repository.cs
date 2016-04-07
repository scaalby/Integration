using System.Collections.Generic;
using System.Linq;
using MicroservicesSpike.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDB.Bson.IO;
using Newtonsoft.Json;


namespace MicroservicesSpike.Repositories
{
    public class Repository : IRepository
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _database;
        MongoCollection<Corporation> _corporation;
        MongoCollection<RealEstate> _realEstate;

        public Repository()
            : this("")
        {
        }

        public Repository(string connection)
        {
            if (string.IsNullOrWhiteSpace(connection))
            {
                connection = "mongodb://notb544:27017";
            }

            _client = new MongoClient(connection);
            _server = _client.GetServer();
            _database = _server.GetDatabase("local", WriteConcern.Unacknowledged);
            _corporation = _database.GetCollection<Corporation>("Corporations");
            _realEstate = _database.GetCollection<RealEstate>("RealEstates");
        }

        public IEnumerable<Corporation> GetAllCorporations()
        {
            return _corporation.FindAll();
        }

        public Corporation GetCorporation(ObjectId Id)

        {
            IMongoQuery query = Query.EQ("_id", Id);
            return _corporation.Find(query).FirstOrDefault();
        }

        public Corporation GetCorporation(string Id)

        {
            var id = ObjectId.Parse(Id);
            return GetCorporation(id);
        }

        public ObjectId AddCorporation(Corporation build)
        {
            build.Id = ObjectId.GenerateNewId();
            _corporation.Insert(build);
            WriteConcernResult x = new WriteConcernResult(response: build.ToBsonDocument());
            return build.Id;
        }

        public void RemoveCorporation(string Id)
        {
            var _id = ObjectId.Parse(Id);
            IMongoQuery query = Query.EQ("_id", _id);
            _corporation.Remove(query);
        }

        public bool UpdateRealEstate(string Id, int position, RealEstate estate)
        {
            var _id = ObjectId.Parse(Id);
            IMongoQuery query = Query.And(Query<Corporation>.EQ(t => t.Id, _id));

            IMongoUpdate update = Update<Corporation>
                .Set(y => y.RealEstates[position].city, estate.city)
                .Set(y => y.RealEstates[position].code, estate.code)
                .Set(y => y.RealEstates[position].state, estate.state)
                .Set(y => y.RealEstates[position].street, estate.street)
                .Set(y => y.RealEstates[position].zip, estate.zip);

            _corporation.Update(query, update);
            //_realEstate.Update(query, update);

     //MANCA LA GESTIONE DELL'ERRORE --- DA FARE
            //WriteConcernResult x = new WriteConcernResult(response: update.ToBsonDocument());
            //if (!x.UpdatedExisting)
            //    return false;

            return true;
        }

        public bool UpdateCorporation(string Id, Corporation build)
        {
            var _id = ObjectId.Parse(Id);
            IMongoQuery query = Query.EQ("_id", _id);
            IMongoUpdate update = Update
                .Set("_id", _id)
                .Set("Name", build.Name)
                .Set("Nation", build.Nation);
           
            _corporation.Update(query, update);

   //MANCA LA GESTIONE DELL'ERRORE --- DA FARE
            //WriteConcernResult x = new WriteConcernResult(response: build.ToBsonDocument());
            //if (x.HasLastErrorMessage)
            //    return false;

            return true;
        }

        public bool AddRealEstate(string id, RealEstate estate)
        { 
            var corporation = GetCorporation(id);

            if (corporation.RealEstates == null)
            {
                corporation.RealEstates = new List<RealEstate>();
            }
            corporation.RealEstates.Add(estate);
            _corporation.Save<Corporation>(corporation);

    //MANCA LA GESTIONE DELL'ERRORE --- DA FARE
            //WriteConcernResult x = new WriteConcernResult(response: corporation.ToBsonDocument());
            //if (x.HasLastErrorMessage)
            //    return false;

            return true;
        }

    }
}