using System;
using ResidentContactApi.V1.Domain;
using FluentAssertions;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Response;

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
        public void EntitiesHaveADateAdded()
        {
            var entity = new ContactDetailsResponse();
            var date = new DateTime(2019, 02, 21);
            entity.DateAdded = date;

            entity.DateAdded.Should().BeSameDateAs(date);
        }
    }
}
