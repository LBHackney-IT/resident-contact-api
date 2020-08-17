using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Requests;
using Bogus;
using System.Text;
using System.Net.Http;

namespace ResidentContactApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class CreateContactRecordTests : IntegrationTests<Startup>
    {
        private readonly Faker _faker = new Faker();

        [Test]
        [Ignore("")]
        public async Task Returns201IfNewContactRecordIsAddedForResident()
        {
            var resident = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext);

            var contactRequest = new ResidentContactParam
            {
                ContactSubTypeLookupId = _faker.Random.Int(1, 5),
                ContactTypeLookupId = _faker.Random.Int(1, 5),
                ContactValue = _faker.Random.String(11, 100),
                IsActive = _faker.Random.Bool(),
                IsDefault = _faker.Random.Bool(),
                ResidentId = _faker.Random.Int(1)
            };

            var url = new Uri($"/api/v1/tokens", UriKind.Relative);
            using var content = new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(url, content).ConfigureAwait(true);
            response.StatusCode.Should().Be(201);
        }

    }
}
