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
            _classUnderTest.GetResidents("bob", "brown").Should().BeEmpty();
        }


        [Test]
        public void GetAllResidentsWithFirstNameParametersMatchingResidents()
        {
            var databaseEntity = AddPersonRecordToDatabase(firstname: "ciasom");
            var databaseEntity1 = AddPersonRecordToDatabase(firstname: "shape");
            var databaseEntity2 = AddPersonRecordToDatabase(firstname: "Ciasom");

            var contactType = AddContactTypeToDatabase();

            var contact = AddContactRecordToDatabase(databaseEntity.Id, contactType.Id);
            var contact1 = AddContactRecordToDatabase(databaseEntity1.Id, contactType.Id);
            var contact2 = AddContactRecordToDatabase(databaseEntity2.Id, contactType.Id);

            var domainEntity = databaseEntity.ToDomain();
            domainEntity.Contacts = new List<ContactDetailsDomain> { contact.ToDomain() };
            domainEntity.Contacts.First().Type = contactType.Name;

            var domainEntity2 = databaseEntity2.ToDomain();
            domainEntity2.Contacts = new List<ContactDetailsDomain> { contact2.ToDomain() };
            domainEntity2.Contacts.First().Type = contactType.Name;

            var listOfPersons = _classUnderTest.GetResidents(firstName: "ciasom");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domainEntity);
            listOfPersons.Should().ContainEquivalentOf(domainEntity2);
        }

        [Test]
        public void GetAllResidentsWithLastNameParametersMatchingResidents()
        {
            var entity = AddPersonRecordToDatabase(lastname: "brown");
            var entity1 = AddPersonRecordToDatabase(lastname: "tessalate");
            var entity2 = AddPersonRecordToDatabase(lastname: "Brown");

            var contactType = AddContactTypeToDatabase();

            var contactEntity = AddContactRecordToDatabase(entity.Id, contactType.Id);
            var contactEntity1 = AddContactRecordToDatabase(entity1.Id, contactType.Id);
            var contactEntity2 = AddContactRecordToDatabase(entity2.Id, contactType.Id);

            var domain = entity.ToDomain();
            domain.Contacts = new List<ContactDetailsDomain> { contactEntity.ToDomain() };
            domain.Contacts.First().Type = contactType.Name;

            var domain2 = entity2.ToDomain();
            domain2.Contacts = new List<ContactDetailsDomain> { contactEntity2.ToDomain() };
            domain2.Contacts.First().Type = contactType.Name;

            var listOfPersons = _classUnderTest.GetResidents(lastName: "brown");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domain);
            listOfPersons.Should().ContainEquivalentOf(domain2);
        }

        [Test]
        public void GetAllResidentWithNoContactWithFirstnameAndLastnameMatchingParameters()
        {
            var databaseEntity = AddPersonRecordToDatabase(firstname: "ciasom", lastname: "brown");
            var databaseEntity1 = AddPersonRecordToDatabase(firstname: "shape", lastname: "tessalate");
            var databaseEntity2 = AddPersonRecordToDatabase(firstname: "Ciasom", lastname: "Brown");

            var domain = databaseEntity.ToDomain();
            domain.Contacts = new List<ContactDetailsDomain>();

            var domain2 = databaseEntity2.ToDomain();
            domain2.Contacts = new List<ContactDetailsDomain>();

            var listOfPersons = _classUnderTest.GetResidents(lastName: "brown");
            listOfPersons.Count.Should().Be(2);
            listOfPersons.Should().ContainEquivalentOf(domain);
            listOfPersons.Should().ContainEquivalentOf(domain2);
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
        [Ignore("")]
        public void InsertingATokenRecordShouldReturnAnId()
        {
            //var tokenRequest = _fixture.Build<TokenRequestObject>().Create();

            //var response = _classUnderTest.GenerateToken(tokenRequest);

            //response.Should().NotBe(0);
        }
        [Test]
        [Ignore("")]
        public void InsertedRecordShouldBeInsertedOnceInTheDatabase()
        {
            //var tokenRequest = _fixture.Build<TokenRequestObject>().Create();

            //var response = _classUnderTest.GenerateToken(tokenRequest);

            //var databaseRecord = DatabaseContext.Tokens.Where(x => x.Id == response);
            //var defaultRecordRetrieved = databaseRecord.FirstOrDefault();

            //databaseRecord.Count().Should().Be(1);
        }
        [Test]
        [Ignore("")]
        public void InsertedRecordShouldBeInTheDatabase()
        {
            //var tokenRequest = _fixture.Build<TokenRequestObject>().Create();

            //var response = _classUnderTest.GenerateToken(tokenRequest);

            //var databaseRecord = DatabaseContext.Tokens.Where(x => x.Id == response);
            //var defaultRecordRetrieved = databaseRecord.FirstOrDefault();

            //defaultRecordRetrieved.RequestedBy.Should().Be(tokenRequest.RequestedBy);
            //defaultRecordRetrieved.Valid.Should().BeTrue();
            //defaultRecordRetrieved.ExpirationDate.Should().Be(tokenRequest.ExpiresAt);
            //defaultRecordRetrieved.DateCreated.Date.Should().Be(DateTime.Now.Date);
            //defaultRecordRetrieved.Environment.Should().Be(tokenRequest.Environment);
            //defaultRecordRetrieved.ConsumerTypeLookupId.Should().Be(tokenRequest.ConsumerType);
            //defaultRecordRetrieved.ConsumerName.Should().Be(tokenRequest.Consumer);
            //defaultRecordRetrieved.AuthorizedBy.Should().Be(tokenRequest.AuthorizedBy);
            //defaultRecordRetrieved.ApiEndpointNameLookupId.Should().Be(tokenRequest.ApiEndpoint);
            //defaultRecordRetrieved.ApiLookupId.Should().Be(tokenRequest.ApiName);
        }

        private Resident AddPersonRecordToDatabase(string lastname = null, string firstname = null)
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity(firstname, lastname);
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
    }

}
