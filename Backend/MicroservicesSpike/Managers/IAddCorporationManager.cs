using MicroservicesSpike.Models;
using MicroservicesSpike.Repositories;
using MongoDB.Bson;

namespace MicroservicesSpike.Managers
{
    public interface IAddCorporationManager
    {
        ObjectId AddCorporation(IRepository repository, Corporation corporation);
        bool AddRealEstate(IRepository repository, string id, RealEstate realEstate);
        bool UpdateCorporation(IRepository repository, string id, Corporation input);
        bool UpdateRealEstate(IRepository repository, string id, int position, RealEstate input);
    }
}
