using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MicroservicesSpike.Managers;
using MicroservicesSpike.Models;
using NUnit.Framework;

namespace MicroServicesSpike.Test
{
    [TestFixture]
    public class FilterRealEstatesTest
    {
        private FilterManager _fm;
        private Corporation _cp;


        [SetUp]
        public void Init()
        {
            _fm = new FilterManager();

        }
    }
}
