using MicroservicesSpike.Managers;
using MicroservicesSpike.Models;
using MicroservicesSpike.Repositories;
using MongoDB.Bson;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;

namespace MicroservicesSpike.Modules
{
    public class DeleteModule : NancyModule
    {
        private IRepository _repo;
        private IFilterManager _filterManager;

        public DeleteModule(IRepository repo, IFilterManager filterManager)
        {
            _repo = repo;
            _filterManager = filterManager;

            Delete["DeleteCorporation/{id}"] = par =>
            {
                _repo.RemoveCorporation(par.id);

                var objID = JsonConvert.SerializeObject(new { id = par.id });
                return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.OK, "Ente cancellato correttamente - id = " + par.id, objID );
            };

            Post["DeleteCorporazione"] = _ =>
            {
                var corp = this.Bind<Corporation>();

                _repo.RemoveCorporation(corp.Id.ToString());

                return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.OK, "Ente cancellato correttamente - id = " + corp.Id.ToString(), corp.Id.ToString());
            };
        }
    }
}
