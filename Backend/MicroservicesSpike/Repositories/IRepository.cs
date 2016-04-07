using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroservicesSpike.Models;
using MongoDB.Bson;


namespace MicroservicesSpike.Repositories
{
    public interface IRepository
    {
        IEnumerable<Corporation> GetAllCorporations();
        Corporation GetCorporation(string id);
        Corporation GetCorporation(ObjectId id);
        ObjectId AddCorporation(Corporation build);
        void RemoveCorporation(string id);
        bool UpdateCorporation(string id,Corporation build);
        //Corporation AddRealEstate(string id, RealEstate estate);
        bool AddRealEstate(string id, RealEstate estate);
        bool UpdateRealEstate(string Id, int code, RealEstate estate);
    }
}
