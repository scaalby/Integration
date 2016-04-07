using System.Collections.Generic;
using System.Linq;
using MicroservicesSpike.Models;
using System;

namespace MicroservicesSpike.Managers
{
    public class FilterManager : IFilterManager
    {
        public List<Corporation> FilterCorporation(List<Corporation> cpList, Corporation cp)
        {
            List<Corporation> cpSearched;
            if (!string.IsNullOrEmpty(cp.Name) && !string.IsNullOrEmpty(cp.Nation))
            {
                cpSearched = cpList.Where(x => x.Name.ToLower().Contains(cp.Name.ToLower()) 
                && x.Nation.ToLower().Contains(cp.Nation.ToLower())).ToList();
                return cpSearched;
            }
            else if (string.IsNullOrEmpty(cp.Nation))
            {
                cpSearched = cpList.Where(x => x.Name.ToLower().Contains(cp.Name.ToLower())).ToList();
                return cpSearched;
            }
            else
            {
                cpSearched = cpList.Where(x => x.Nation.ToLower().Contains(cp.Nation.ToLower())).ToList();
                return cpSearched;
            }
        }

        //Work In Progress
        public List<RealEstate> FilterRealEstates(Corporation cp, RealEstate re)
        {
            List<RealEstate> resultList = new List<RealEstate>();

            foreach (var item in cp.RealEstates)
            {
                bool ToAdd = true;

                if (ToAdd && re.code != 0)
                { if (!item.code.Equals(re.code)) ToAdd = false; }

                if (ToAdd && re.zip != 0)
                { if (!item.zip.Equals(re.zip)) ToAdd = false; }

                if (ToAdd && !String.IsNullOrWhiteSpace(re.city))
                {
                    if (item.city == null)
                    {
                        ToAdd = false;
                    }
                    else 
                        if (!item.city.ToLower().Contains(re.city.ToLower())) ToAdd = false;
                }

                if (ToAdd && !String.IsNullOrWhiteSpace(re.state))
                {
                    if (item.state == null)
                    {
                        ToAdd = false;
                    }
                    else
                        if (!item.state.ToLower().Contains(re.city.ToLower())) ToAdd = false;
                }

                if (ToAdd && !String.IsNullOrWhiteSpace(re.street))
                {
                    if (item.street == null)
                    {
                        ToAdd = false;
                    }
                    else
                        if (!item.street.ToLower().Contains(re.street.ToLower())) ToAdd = false;
                }

                if (ToAdd == true)
                {
                    resultList.Add(item);
                }
            }

            return resultList;
        }
    }
}