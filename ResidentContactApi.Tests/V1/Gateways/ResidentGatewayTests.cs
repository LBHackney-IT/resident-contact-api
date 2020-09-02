using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Gateways;
using FluentAssertions;
using NUnit.Framework;
using ResidentContactApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using ResidentContactApi.V1.Enums;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Boundary.Requests;

namespace ResidentContactApi.Tests.V1.Gateways
{
    [TestFixture]
    public class ResidentGatewayTests : DatabaseTests
    {
        private ResidentGateway _classUnderTest;
        private Fixture _fixture = new Fixture();

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
            _classUnderTest.GetResidents(20, 0, "bob", "brown").Should().BeEmpty();
        }

        [Test]
        public void GetAllResidentsWithFirstNameParametersMatchingResidents()
        {
            var domainEntity1 = AddResidentAndContactDetailsToDatabase("ciasom");
            var domainEntity2 = AddResidentAndContactDetailsToDatabase("shape");
            var domainEntity3 = AddResidentAndContactDetailsToDatabase("Ciasom");

            var listOfPersons = _classUnderTest.GetResidents(20, 0, firstName: "ciasom");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domainEntity1);
            listOfPersons.Should().ContainEquivalentOf(domainEntity3);
        }

        [Test]
        public void GetAllResidentsWithLastNameParametersMatchingResidents()
        {
            var domain1 = AddResidentAndContactDetailsToDatabase(lastName: "brown");
            var domain2 = AddResidentAndContactDetailsToDatabase(lastName: "tessalate");
            var domain3 = AddResidentAndContactDetailsToDatabase(lastName: "Brown");

            var listOfPersons = _classUnderTest.GetResidents(20, 0, lastName: "brown");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domain1);
            listOfPersons.Should().ContainEquivalentOf(domain3);
        }

        [Test]
        public void GetAllResidentWithNoContactWithFirstnameAndLastnameMatchingParameters()
        {
            var databaseEntity = AddPersonRecordToDatabase(firstName: "ciasom", lastName: "brown");
            var domain = databaseEntity.ToDomain();
            domain.Contacts = new List<ContactDetailsDomain>();

            var databaseEntity1 = AddPersonRecordToDatabase(firstName: "shape", lastName: "tessalate");

            var databaseEntity2 = AddPersonRecordToDatabase(firstName: "Ciasom", lastName: "Brown");
            var domain2 = databaseEntity2.ToDomain();
            domain2.Contacts = new List<ContactDetailsDomain>();

            var listOfPersons = _classUnderTest.GetResidents(limit: 20, cursor: 0, lastName: "brown");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domain);
            listOfPersons.Should().ContainEquivalentOf(domain2);
        }

        [Test]
        public void GetAllResidentsWillReturnLimitNumberOfRecordsReturned()
        {
            AddResidentAndContactDetailsToDatabase();
            AddResidentAndContactDetailsToDatabase();
            AddResidentAndContactDetailsToDatabase();

            var response = _classUnderTest.GetResidents(limit: 2, 0);
            response.Count.Should().Be(2);
        }

        [Test]
        public void GetAllResidentsWillReturnRecordsOrderedByResidentId()
        {
            var resident2 = AddResidentAndContactDetailsToDatabase(id: 45);
            var resident3 = AddResidentAndContactDetailsToDatabase(id: 87);
            var resident1 = AddResidentAndContactDetailsToDatabase(id: 2);

            var response = _classUnderTest.GetResidents(limit: 2, 0);
            response.Count.Should().Be(2);
            response.First()
                .Should().BeEquivalentTo(resident1);
            response.Last()
                .Should().BeEquivalentTo(resident2);
        }

        [Test]
        public void GetAllResidentsWillOffsetByGivenCursor()
        {
            var resident2 = AddResidentAndContactDetailsToDatabase(id: 45);
            var resident3 = AddResidentAndContactDetailsToDatabase(id: 87);
            var resident1 = AddResidentAndContactDetailsToDatabase(id: 2);
            var resident4 = AddResidentAndContactDetailsToDatabase(id: 762554);

            var response = _classUnderTest.GetResidents(limit: 2, cursor: 45);
            response.Count.Should().Be(2);
            response.First().Should().BeEquivalentTo(resident3);
            response.Last().Should().BeEquivalentTo(resident4);
        }


        [Test]
        public void GetResidentByIdWhenNoMatchingRecordReturnNull()
        {
            var response = _classUnderTest.GetResidentById(123);

            response.Should().BeNull();
        }

        [Test]
        public void GetResidentByIdReturnsResidentDetails()
        {
            var databaseEntity = AddPersonRecordToDatabase();
            var response = _classUnderTest.GetResidentById(databaseEntity.Id);

            response.FirstName.Should().Be(databaseEntity.FirstName);
            response.LastName.Should().Be(databaseEntity.LastName);
            response.Gender.Should().Be(GenderTypeEnum.F);
            response.Should().NotBe(null);
        }

        [Test]
        public void GetResidentByIdReturnsContactDetailsWithTypeAndSubtypeName()
        {
            var databaseEntity = AddPersonRecordToDatabase();
            var contactType = AddContactTypeToDatabase();
            var contact = AddContactRecordToDatabase(databaseEntity.Id, contactType.Id);

            var response = _classUnderTest.GetResidentById(databaseEntity.Id);

            var expectedDomainResponse = contact.ToDomain();
            expectedDomainResponse.Type = contactType.Name;

            response.Contacts.Should().BeEquivalentTo(new List<ContactDetailsDomain> { expectedDomainResponse });
        }

        [Test]
        public void InsertedContactRecordShouldBeInsertedOnceInTheDatabase()
        {
            var databaseEntity = AddPersonRecordToDatabase();
            var contactType = AddContactTypeToDatabase();
            var contact = AddContactRecordToDatabase(databaseEntity.Id, contactType.Id);

            var request = _fixture.Build<ResidentContact>()
                .With(x => x.ResidentId, databaseEntity.Id)
                .With(x => x.ContactTypeLookupId, contactType.Id)
                .Without(x => x.ContactSubTypeLookupId)
                .Create();

            var response = _classUnderTest.InsertResidentContactDetails(request);
            var databaseRecord = ResidentContactContext.Residents.Where(res => res.Id == response.Id);

            var record = databaseRecord.First();

            databaseRecord.Count().Should().Be(1);
            record.Id.Should().Be(request.ResidentId);
            record.Contacts.Count.Should().Be(2);
        }

        private Resident AddPersonRecordToDatabase(string lastName = null, string firstName = null, int? id = null)
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity(firstName, lastName, id);
            ResidentContactContext.Residents.Add(databaseEntity);
            ResidentContactContext.SaveChanges();
            return databaseEntity;
        }

        private Contact AddContactRecordToDatabase(int residentId, int contactTypeId)
        {
            var contact = TestHelper.CreateDatabaseContactEntity(residentId, contactTypeId);
            ResidentContactContext.ContactDetails.Add(contact);
            ResidentContactContext.SaveChanges();
            return contact;
        }

        private ContactTypeLookup AddContactTypeToDatabase()
        {
            var contactType = new ContactTypeLookup { Name = _fixture.Create<string>() };
            ResidentContactContext.ContactTypeLookups.Add(contactType);
            ResidentContactContext.SaveChanges();
            return contactType;
        }

        private ResidentDomain AddResidentAndContactDetailsToDatabase(string firstName = null, string lastName = null, int? id = null)
        {
            var databaseEntity = AddPersonRecordToDatabase(firstName: firstName, id: id, lastName: lastName);
            var domainEntity = databaseEntity.ToDomain();
            var contactType = AddContactTypeToDatabase();
            var contact = AddContactRecordToDatabase(databaseEntity.Id, contactType.Id);
            domainEntity.Contacts = new List<ContactDetailsDomain> { contact.ToDomain() };
            domainEntity.Contacts.First().Type = contactType.Name;
            return domainEntity;
        }
    }

}
