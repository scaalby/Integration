using MicroservicesSpike.Managers;
using MicroservicesSpike.Models;
using MongoDB.Bson;
using NUnit.Framework;
using System.Collections.Generic;

namespace MicroServicesSpike.Test
{
    [TestFixture]
    public class FilterRealEstateTest
    {
        private FilterManager _fm;
        private Corporation _cp;

        [SetUp]
        public void Init()
        {
            _fm = new FilterManager();
            _cp = new Corporation() {
                Id = ObjectId.Parse("564db26db99f725971d81657"),
                Name = "EvilCorp",
                Nation = "Italy",
                RealEstates = new List<RealEstate>()
                {
                    new RealEstate() {
                        code = 1,
                        state = "Ungar",
                        street = "noob"
                    },
                    new RealEstate() {
                        code = 2,
                        state = "italy",
                        zip = 123
                    }, 
                    new RealEstate(){ },
                    new RealEstate() {
                    code = 3,
                    street = "ciao",
                    state = "canad",
                    zip = 12312,
                    city = "sanfelicepaninaro"
                    },
                    new RealEstate(){ code = 2 },
                }
            };
        }

        [TestCase(5,true)]
        [TestCase(3,false)]
        [TestCase(6,false)]
        public void FilterRealEstates_ReturnTheWholeList(int numberReturned, bool expected)
        {
            var re = new RealEstate();

            var list = _fm.FilterRealEstates(_cp, re);

            Assert.AreEqual(expected ,list.Count == numberReturned);
        }

        [TestCase(1,1,true)]
        [TestCase(2,1,false)]
        [TestCase(2,2,true)]
        public void FilterRealEstates_ReturnOneElement(int numberReturned,int codePassed, bool expected)
        {
            var re = new RealEstate() {
                code = codePassed
            };

            var list = _fm.FilterRealEstates(_cp, re);

            Assert.AreEqual(expected, list.Count == numberReturned);
        }
    }
}
