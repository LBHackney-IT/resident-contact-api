using ResidentContactApi.V1.Factories;
using FluentAssertions;
using NUnit.Framework;
using AutoFixture;
using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Domain;
using Bogus;
using System;

namespace ResidentContactApi.Tests.V1.Factories
{
    [TestFixture]
    public class EntityFactoryTest
    {
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public void ItMapsAPersonDatabaseRecordIntoResidentInformationDomainObject()
        {
            var personRecord = TestHelper.CreateDatabasePersonEntity();
            var domain = personRecord.ToDomain();
            domain.Should().BeEquivalentTo(new ResidentDomain
            {
                FirstName = personRecord.FirstName,
                LastName = personRecord.LastName,
                DateOfBirth = new DateTime(),
                Gender = personRecord.Gender
            });
        }





    }
}
