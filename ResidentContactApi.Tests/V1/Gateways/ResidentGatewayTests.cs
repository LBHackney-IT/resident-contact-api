using AutoFixture;
using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Gateways;
using FluentAssertions;
using NUnit.Framework;
using System.ComponentModel.Design.Serialization;
using ResidentContactApi.V1.Boundary.Response.Residents;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using ResidentContactApi.V1.Infrastructure;
using System.Collections.Generic;
using ResidentContactApi.V1.Factories;

namespace ResidentContactApi.Tests.V1.Gateways
{
    [TestFixture]
    public class ResidentGatewayTests : DatabaseTests
    {
        private ResidentGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new ResidentGateway(ResidentContactContext);
        }

        [Test]
        public void GatewayImplementsBoundaryInterface()
        {
            Assert.NotNull(_classUnderTest is IResidentGateway);
        }

        [Test]
        public void GetAllResidentsIfThereAreNoResidentsReturnsAnEmptyList()
        {
            _classUnderTest.GetResidents("bob", "brown").Should().BeEmpty();
        }

        [Test]
        public void GetAllResidentsWithFirstNameParametersMatchingResidents()
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity(firstname: "ciasom");
            var databaseEntity1 = TestHelper.CreateDatabasePersonEntity(firstname: "shape");
            var databaseEntity2 = TestHelper.CreateDatabasePersonEntity(firstname: "Ciasom");

            var personslist = new List<ResidentsInfra>
            {
                databaseEntity,
                databaseEntity1,
                databaseEntity2
            };

            ResidentContactContext.Residents.AddRange(personslist);
            ResidentContactContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetResidents(firstName: "ciasom");
            listOfPersons.Count.Should().Be(2);
        }

        [Test]
        public void GetAllResidentsWithLastNameParametersMatchingResidents()
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity(lastname: "brown");
            var databaseEntity1 = TestHelper.CreateDatabasePersonEntity(lastname: "tessalate");
            var databaseEntity2 = TestHelper.CreateDatabasePersonEntity(lastname: "Brown");

            var personslist = new List<ResidentsInfra>
            {
                databaseEntity,
                databaseEntity1,
                databaseEntity2
            };

            ResidentContactContext.Residents.AddRange(personslist);
            ResidentContactContext.SaveChanges();

            var listOfPersons = _classUnderTest.GetResidents(lastName: "brown");
            listOfPersons.Count.Should().Be(2);
        }


    }
}
