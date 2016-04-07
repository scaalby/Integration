using System.Linq;
using MicroservicesSpike.Managers;
using MicroservicesSpike.Models;
using MicroservicesSpike.Repositories;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;

namespace MicroservicesSpike.Modules
{
    public class FilterModule : NancyModule
    {
        private IRepository _rp;
        private IFilterManager _fm;

        public FilterModule(IRepository rp, IFilterManager fm)
        {
            _rp = rp;
            _fm = fm;

            Get["GetCorporations"] = _ =>
            {
                var input = this.Bind<Corporation>();
                var cpList = _rp.GetAllCorporations().ToList();

                if (input.Name == null && input.Nation == null)
                {
                    return JsonConvert.SerializeObject(cpList);
                }

                var filtred = _fm.FilterCorporation(cpList, input);
                return JsonConvert.SerializeObject(filtred);
            };

            Get["GetSingleCorp"] = _ =>
            {
                var input = this.Bind<Corporation>();
                var corpSearched = _rp.GetCorporation(input.Id);
                return JsonConvert.SerializeObject(corpSearched);
            };

            Get["GetRealEstates/{id}"] = par =>
            {
                var real = this.Bind<RealEstate>();
                Corporation corpInterested = _rp.GetCorporation(par.id);

                if (real.code == 0 && string.IsNullOrEmpty(real.city) && string.IsNullOrEmpty(real.state)
                && string.IsNullOrEmpty(real.street) && real.zip == 0)
                {
                    return JsonConvert.SerializeObject(corpInterested.RealEstates);
                }

                var reList = _fm.FilterRealEstates(corpInterested, real);
                return JsonConvert.SerializeObject(reList);
            };
        }
    }
}