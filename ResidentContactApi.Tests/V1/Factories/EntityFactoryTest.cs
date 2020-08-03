using ResidentContactApi.V1.Factories;
using FluentAssertions;
using NUnit.Framework;
using AutoFixture;
using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Domain;
using Bogus;
using System;
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
    }
}
