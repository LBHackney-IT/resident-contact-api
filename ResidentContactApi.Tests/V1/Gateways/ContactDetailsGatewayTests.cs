using System;
using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Gateways;
using FluentAssertions;
using NUnit.Framework;
using ResidentContactApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using FluentAssertions.Equivalency;
using ResidentContactApi.V1.Enums;
using ResidentContactApi.V1.Factories;

namespace ResidentContactApi.Tests.V1.Gateways
{
    [TestFixture]
    public class ContactDetailsGatewayTests : DatabaseTests
    {
        private ContactDetailsGateway _classUnderTest;
        private Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(2));
            _classUnderTest = new ContactDetailsGateway(ResidentContactContext);
        }

        [Test]
        public void GatewayImplementsBoundaryInterface()
        {
            Assert.NotNull(_classUnderTest is IContactDetailsGateway);
        }

        [Test]
        public void GetContactByIdReturnsNullWithNoRecords()
        {
            Assert.Null(_classUnderTest.getContactById(123));
        }

        [Test]
        public void GetContactByIdReturnsCorrectRecord()
        {
            var domainEntity1 = AddResidentAndContactDetailsToDatabase("firstname");

            Assert.AreEqual(domainEntity1.Contacts.FirstOrDefault().Id, _classUnderTest.getContactById(domainEntity1.Contacts.FirstOrDefault().Id).Id);
        }

        [Test]
        public void GetContactByIdReturnsCorrectRecordMultipleResidents()
        {
            var domainEntity1 = AddResidentAndContactDetailsToDatabase("firstname");
            var domainEntity2 = AddResidentAndContactDetailsToDatabase("firstname");

            Assert.AreEqual(domainEntity1.Contacts.FirstOrDefault().Id, _classUnderTest.getContactById(domainEntity1.Contacts.FirstOrDefault().Id).Id);
            Assert.AreEqual(domainEntity2.Contacts.FirstOrDefault().Id, _classUnderTest.getContactById(domainEntity2.Contacts.FirstOrDefault().Id).Id);
        }
        
        [Test]
        public void GetContactByIdReturnsCorrectRecordMultipleContactsPerResident()
        {
            var domainEntity1 = AddResidentAndContactDetailsToDatabase("firstname");
            var contactType = AddContactTypeToDatabase();
            var contact = AddContactRecordToDatabase(domainEntity1.Id, contactType.Id);

            Assert.AreEqual(domainEntity1.Contacts.FirstOrDefault().Id, _classUnderTest.getContactById(domainEntity1.Contacts.FirstOrDefault().Id).Id);
            Assert.AreEqual(contact.Id, _classUnderTest.getContactById(contact.Id).Id);
        }

        //NB. these come from ResidentGatewayTests and should be abstracted properly
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
