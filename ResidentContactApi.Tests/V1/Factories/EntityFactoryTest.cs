using System;
using ResidentContactApi.V1.Factories;
using FluentAssertions;
using NUnit.Framework;
using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Domain;
using System.Collections.Generic;
using ResidentContactApi.V1.Enums;
using ResidentContactApi.V1.Infrastructure;

namespace ResidentContactApi.Tests.V1.Factories
{
    [TestFixture]
    public class EntityFactoryTest
    {
        [Test]
        public void ItMapsAPersonDatabaseRecordIntoResidentInformationDomainObject()
        {
            var personRecord = TestHelper.CreateDatabasePersonEntity();
            var contacts = new List<Contact> { TestHelper.CreateDatabaseContactEntity(1) };
            personRecord.Contacts = contacts;
            var domain = personRecord.ToDomain();
            domain.Should().BeEquivalentTo(new ResidentDomain
            {
                Id = personRecord.Id,
                FirstName = personRecord.FirstName,
                LastName = personRecord.LastName,
                DateOfBirth = personRecord.DateOfBirth,
                Gender = GenderTypeEnum.F,
                Contacts = contacts.ToDomain()
            });
        }

        [Test]
        public void ItMapsAPersonDatabaseRecordWithMinimalInfoIntoResidentInformationDomainObject()
        {
            var personRecord = new Resident { Id = 1 };
            var domain = personRecord.ToDomain();
            domain.Should().BeEquivalentTo(new ResidentDomain
            {
                Id = 1,
                Gender = GenderTypeEnum.Unknown
            });
        }

        [Test]
        public void ItMapsAContactDatabaseRecordToDomain()
        {
            var contactType = new ContactTypeLookup
            {
                Id = 2,
                Name = "My type Name"
            };
            var contactSubType = new ContactSubTypeLookup
            {
                Id = 5,
                Name = "Mobile sub type"
            };
            var contact = new Contact
            {
                Id = 5,
                AddedBy = "adder",
                ContactValue = "0284628",
                DateAdded = new DateTime(2012, 04, 05),
                IsActive = true,
                IsDefault = false,
                ModifiedBy = "modifier",
                ContactTypeLookup = contactType,
                DateLastModified = new DateTime(2013, 07, 12),
                ContactSubTypeLookup = contactSubType,
            };
            contact.ToDomain().Should().BeEquivalentTo(new ContactDetailsDomain
            {
                Id = 5,
                Type = "My type Name",
                AddedBy = "adder",
                ContactValue = "0284628",
                DateAdded = new DateTime(2012, 04, 05),
                IsActive = true,
                IsDefault = false,
                ModifiedBy = "modifier",
                SubType = "Mobile sub type",
                DateLastModified = new DateTime(2013, 07, 12)
            });
        }
    }
}
