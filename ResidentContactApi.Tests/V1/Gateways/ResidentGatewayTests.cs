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
using Bogus;

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

            var personslist = new List<Resident>
            {
                databaseEntity,
                databaseEntity1,
                databaseEntity2
            };

            ResidentContactContext.Residents.AddRange(personslist);
            ResidentContactContext.SaveChanges();

            var contact = TestHelper.CreateDatabaseContactEntity(databaseEntity.Id);
            var contact1 = TestHelper.CreateDatabaseContactEntity(databaseEntity1.Id);
            var contact2 = TestHelper.CreateDatabaseContactEntity(databaseEntity2.Id);

            var contactLists = new List<Contact>
            {
                contact,
                contact1,
                contact2
            };

            ResidentContactContext.ContactDetails.AddRange(contactLists);
            ResidentContactContext.SaveChanges();

            var domainEntity = databaseEntity.ToDomain();
            domainEntity.Contacts = new List<ContactDetailsDomain> { contact.ToDomain() };


            var domainEntity2 = databaseEntity2.ToDomain();
            domainEntity2.Contacts = new List<ContactDetailsDomain> { contact2.ToDomain() };


            var listOfPersons = _classUnderTest.GetResidents(firstName: "ciasom");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domainEntity);
            listOfPersons.Should().ContainEquivalentOf(domainEntity2);


        }

        [Test]
        public void GetAllResidentsWithLastNameParametersMatchingResidents()
        {
            var entity = TestHelper.CreateDatabasePersonEntity(lastname: "brown");
            var entity1 = TestHelper.CreateDatabasePersonEntity(lastname: "tessalate");
            var entity2 = TestHelper.CreateDatabasePersonEntity(lastname: "Brown");

            var personslist = new List<Resident>
            {
                entity,
                entity1,
                entity2
            };

            ResidentContactContext.Residents.AddRange(personslist);
            ResidentContactContext.SaveChanges();

            var contactEntity = TestHelper.CreateDatabaseContactEntity(entity.Id);
            var contactEntity1 = TestHelper.CreateDatabaseContactEntity(entity1.Id);
            var contactEntity2 = TestHelper.CreateDatabaseContactEntity(entity2.Id);

            var contactList = new List<Contact>
            {
                contactEntity,
                contactEntity1,
                contactEntity2
            };

            ResidentContactContext.ContactDetails.AddRange(contactList);
            ResidentContactContext.SaveChanges();

            var domain = entity.ToDomain();
            domain.Contacts = new List<ContactDetailsDomain> { contactEntity.ToDomain() };


            var domain2 = entity2.ToDomain();
            domain2.Contacts = new List<ContactDetailsDomain> { contactEntity2.ToDomain() };



            var listOfPersons = _classUnderTest.GetResidents(lastName: "brown");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domain);
            listOfPersons.Should().ContainEquivalentOf(domain2);

        }

        [Test]
        public void GetAllResidentWithNoContactWithFirstnameAndLastnameMatchingParameters()
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity(firstname: "ciasom", lastname: "brown");
            var databaseEntity1 = TestHelper.CreateDatabasePersonEntity(firstname: "shape", lastname: "tessalate");
            var databaseEntity2 = TestHelper.CreateDatabasePersonEntity(firstname: "Ciasom", lastname: "Brown");

            var personslist = new List<Resident>
            {
                databaseEntity,
                databaseEntity1,
                databaseEntity2
            };

            ResidentContactContext.Residents.AddRange(personslist);
            ResidentContactContext.SaveChanges();

            var domain = databaseEntity.ToDomain();
            domain.Contacts = new List<ContactDetailsDomain>();

            var domain2 = databaseEntity2.ToDomain();
            domain2.Contacts = new List<ContactDetailsDomain>();

            var listOfPersons = _classUnderTest.GetResidents(lastName: "brown");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domain);
            listOfPersons.Should().ContainEquivalentOf(domain2);

        }



    }
}
