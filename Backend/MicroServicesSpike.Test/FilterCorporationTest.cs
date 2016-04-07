using System.Collections.Generic;
using System.Linq;
using MicroservicesSpike.Managers;
using MicroservicesSpike.Models;
using MicroservicesSpike.Repositories;
using NUnit.Framework;

namespace MicroServicesSpike.Test
{
    [TestFixture]
    class FilterCorporationTest
    {
        private FilterManager _fm;
        private Corporation _cp;
        private List<Corporation> _listCorp;

        [SetUp]
        public void Init()
        {
            _fm = new FilterManager();
            _listCorp = new List<Corporation>()
            {
                new Corporation()
                {
                    Name = "MacDonato",
                    Nation = "Italia"
                },
                new Corporation()
                {
                    Name = "Ciao",
                    Nation = "Rubin"
                }
            };
        }

        [TestCase("Wrong",null,false)]
        [TestCase("MacDonato",null,true)]
        [TestCase("MacDonato","Italia",true)]
        [TestCase("","",false)]
        public void FilterCorporation_NameTesting(string name, string nation, bool expected)
        {
            _cp = new Corporation() {Name = name , Nation = nation};

            var res = _fm.FilterCorporation(_listCorp, _cp);

            if (res.Count == 0)
            {
                Assert.AreEqual(expected, false);    
            }
            else
                Assert.AreEqual(expected, res.First().Name == _cp.Name);
        }

        [TestCase(null,"Mongolia", false)]
        [TestCase("MacDonato", "Italia", true)]
        [TestCase("", "", false)]
        public void FilterCorporation_NationTesting(string name, string nation, bool expected)
        {
            _cp = new Corporation() { Name = name, Nation = nation };

            var res = _fm.FilterCorporation(_listCorp, _cp);

            if (res.Count == 0)
            {
                Assert.AreEqual(expected, false);
            }
            else
                Assert.AreEqual(expected, res.First().Nation == _cp.Nation);
        }

        [TestCase("ma","",true)]
        [TestCase(null,"it",true)]
        [TestCase("mac", "it", true)]
        public void FilterCorporation_SubStringTest(string name, string nation, bool expected)
        {
            _cp = new Corporation() { Name = name, Nation = nation };

            var res = _fm.FilterCorporation(_listCorp, _cp);

            if (res.Count == 0)
            {
                Assert.AreEqual(expected, false);
            }
            else
                Assert.AreEqual(expected, res.First().Name == "MacDonato");
        }
    }
}
