using System;
using System.IO;
using MicroservicesSpike.Managers;
using MicroservicesSpike.Models;
using MicroservicesSpike.Repositories;
using MongoDB.Bson;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using Nancy.IO;
using HttpStatusCode = Nancy.HttpStatusCode;

namespace MicroservicesSpike.Modules
{

    public class AddModule : NancyModule

    {
        private IRepository _rp;
        private IAddCorporationManager _acm;

        public AddModule(IRepository rp,
                         IAddCorporationManager acm)
        {
            _rp = rp;
            _acm = acm;

            Post["AddCorporation"] = _ =>
            {
                var input = this.Bind<Corporation>();

                if (input.Name == null && input.Nation == null)
                {
                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.BadRequest, "Impossibile aggiungere Ente - valore input nullo", null);
                }

                var id = _acm.AddCorporation(_rp, input);
                var cont = JsonConvert.SerializeObject(new { id = id });

                return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.Created, "Ente aggiunto correttamente - id = " + id.ToString(), cont);
            };

            Put["AddRealEstate/{id}"] = par =>
            {
                var input = this.Bind<RealEstate>();

                if (input.city == null && input.code == 0
                    && input.state == null && input.street == null && input.zip == 0)
                {
                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.BadRequest, "Impossibile aggiungere Real Estate - valore input nullo", null);
                }
                
                var result = _acm.AddRealEstate(_rp, par.id, input);
                
                if (result)
                {
                    var cont = JsonConvert.SerializeObject(new { id = ObjectId.Parse(par.id) });
                    
                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.OK, "Real Estate aggiunta correttamente - id = " + par.id, cont);
                }
                else
                {
                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.InternalServerError, "Impossibile aggiungere Real Estate - fallita aggiunta Real Estate - id = " + par.id, null);
                }
            };

            Put["UpdateCorporation/{id}"] = par =>
            {
                var input = this.Bind<Corporation>();

                if (input.Name == null && input.Nation == null)
                {
                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.BadRequest, "Impossibile aggiornare Ente - valore input nullo", null);
                }

                var result = _acm.UpdateCorporation(_rp, par.id, input);

                if (result)
                {
                    var cont = JsonConvert.SerializeObject(new { id = ObjectId.Parse(par.id) });

                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.OK, "Ente aggiornato correttamente - id = " + par.id, cont);
                }
                else
                {
                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.InternalServerError, "Impossibile aggiornare l'Ente - fallito aggiornamento Ente - id = " + par.id, null);
                }
            };

            Put["UpdateRealEstate/{id}/{position}"] = par =>
            {
                var input = this.Bind<RealEstate>();

                if (input.city == null && input.code == 0
                    && input.state == null && input.street == null && input.zip == 0)
                {
                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.BadRequest, "Impossibile aggiornare RealEstate - valore input nullo", null);
                }

                var result = _acm.UpdateRealEstate(_rp, par.id, par.position, input);

                if (result)
                {
                    var cont = JsonConvert.SerializeObject(new { id = ObjectId.Parse(par.id) });

                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.OK, "RealEstate aggiornata correttamente - id = " + par.id, cont);
                }
                else
                {
                    return SetNancyResponse.NancyResponse(Nancy.HttpStatusCode.InternalServerError, "Impossibile aggiornare RealEstate - fallito aggiornamento RealEstate - id = " + par.id, null);
                }
            };
        }
    }
}
