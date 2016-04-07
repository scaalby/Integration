using MicroservicesSpike.Managers;
using MicroservicesSpike.Models;
using MicroservicesSpike.Repositories;
using MongoDB.Bson;
using Moq;
using NUnit.Framework;

namespace MicroServicesSpike.Test
{
    [TestFixture]
    class AddCorporationTest
    {
        private Mock<IRepository >_repositoryMock;
        private AddCorporationManager _addCorporationManager;
        private Corporation _cp;

        [SetUp]
        public void Init()
        {
            _repositoryMock = new Mock<IRepository>();
            _addCorporationManager = new AddCorporationManager();
            _cp = new Corporation { Name = "Telecom", Nation = "Austria" } ;
            _repositoryMock.Setup(x => x.AddCorporation(It.IsAny<Corporation>())).Returns(
                                new ObjectId("564db26db99f725971d81657"));
        }
        [Test]
        public void AddCorporationTestReturn()
        {
            var result = _addCorporationManager.AddCorporation(_repositoryMock.Object, _cp);
            Assert.AreEqual("564db26db99f725971d81657", result.ToString());
        }

    }
}
