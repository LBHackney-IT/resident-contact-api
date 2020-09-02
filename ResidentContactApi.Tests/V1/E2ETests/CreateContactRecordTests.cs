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
                SubtypeId = _faker.Random.Int(1, 50),
                TypeId = _faker.Random.Int(1, 50),
                Value = "test@test",
                Active = _faker.Random.Bool(),
                Default = _faker.Random.Bool()
            };


            var resident = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext,
                contactTypeLookupId: contactRequest.TypeId,
                contactSubTypeLookupId: contactRequest.SubtypeId);

            contactRequest.ResidentId = resident.Id;
            string json = JsonConvert.SerializeObject(contactRequest);
            var url = new Uri($"/api/v1/contact-details/", UriKind.Relative);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(url, content).ConfigureAwait(true);

            response.StatusCode.Should().Be(201);
        }


        [Test]
        public async Task Returns400IfResidentContactParamModelStateIsInvalid()
        {
            var contactRequest = new ResidentContact
            {
                Value = null
            };

            var url = new Uri($"/api/v1/contact-details", UriKind.Relative);
            using var content = new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(url, content).ConfigureAwait(true);
            response.StatusCode.Should().Be(400);
        }

    }
}
