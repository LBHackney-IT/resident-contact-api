using System;
using ResidentContactApi.V1.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace ResidentContactApi.Tests.V1.Domain
{
    [TestFixture]
    public class EntityTests
    {
        [Test]
        public void EntitiesHaveAnId()
        {
            var entity = new ContactDetailsDomain();
            entity.Id.Should().BeGreaterOrEqualTo(0);
        }

        [Test]
        public void EntitiesHaveACreatedAt()
        {
            //var entity = new ContactDetailsResponse();
            //var date = new DateTime(2019, 02, 21);
            //entity.CreatedAt = date;

            //entity.CreatedAt.Should().BeSameDateAs(date);
        }
    }
}
