using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroservicesSpike.Models;

namespace MicroservicesSpike.Managers
{
    public interface IFilterManager
    {
        List<Corporation> FilterCorporation(List<Corporation> cpList, Corporation cp);
        List<RealEstate> FilterRealEstates(Corporation cp, RealEstate re);
    }
}
