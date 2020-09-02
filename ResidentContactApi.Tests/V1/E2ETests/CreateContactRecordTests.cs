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
        public async Task Returns201IfNewContactRecordIsCreatedForResident()
        {
            var contactRequest = new ResidentContact
            {
                ContactSubTypeLookupId = _faker.Random.Int(1, 50),
                ContactTypeLookupId = _faker.Random.Int(1, 50),
                ContactValue = _faker.Random.String(11, 100),
                IsActive = _faker.Random.Bool(),
                IsDefault = _faker.Random.Bool()
            };

            var resident = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext,
                contactTypeLookupId: contactRequest.ContactTypeLookupId,
                contactSubTypeLookupId: contactRequest.ContactSubTypeLookupId);

            contactRequest.ResidentId = resident.Id;

            var url = new Uri($"/api/v1/contact-details", UriKind.Relative);
            using var content = new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(url, content).ConfigureAwait(true);

            response.StatusCode.Should().Be(201);
        }


        [Test]
        public async Task Returns400IfResidentContactParamModelStateIsInvalid()
        {
            var contactRequest = new ResidentContact
            {
                ContactValue = null
            };

            var url = new Uri($"/api/v1/contact-details", UriKind.Relative);
            using var content = new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(url, content).ConfigureAwait(true);
            response.StatusCode.Should().Be(400);
        }

    }
}
