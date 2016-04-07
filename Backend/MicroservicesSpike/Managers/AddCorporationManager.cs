using MicroservicesSpike.Models;
using MicroservicesSpike.Repositories;
using MongoDB.Bson;
using System.Linq;

namespace MicroservicesSpike.Managers
{
    public class AddCorporationManager : IAddCorporationManager
    {
        public ObjectId AddCorporation(IRepository repository, Corporation corporation)
        {
            return repository.AddCorporation(corporation);
        } 

        public bool AddRealEstate(IRepository repository, string id, RealEstate realEstate)
        {
            var result = repository.AddRealEstate(id, realEstate);

            return result;
        }

        public bool UpdateCorporation(IRepository repository, string id, Corporation corporation)
        {
            return repository.UpdateCorporation(id, corporation);
        }

        public bool UpdateRealEstate(IRepository repository, string id, int position, RealEstate realEstate)
        {
            var result = repository.UpdateRealEstate(id, position, realEstate);

            return result;
        }
    }
}