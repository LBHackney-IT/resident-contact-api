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
        public async Task Returns201IfNewContactRecordIsAddedForResident()
        {
            var contactRequest = new ResidentContactParam
            {
                ContactSubTypeLookupId = _faker.Random.Int(1, 50),
                ContactTypeLookupId = _faker.Random.Int(1, 50),
                ContactValue = _faker.Random.String(11, 100),
                IsActive = _faker.Random.Bool(),
                IsDefault = _faker.Random.Bool()
            };

            var resident = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext,
                contactTypeLookupId: contactRequest.ContactTypeLookupId,
                contactSubTypeLookupId: contactRequest.ContactSubTypeLookupId);

            ResidentContactContext.Database.BeginTransaction();

            contactRequest.ResidentId = resident.Id;

            var url = new Uri($"/api/v1/contact-details", UriKind.Relative);
            var content = new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content).ConfigureAwait(true);
            content.Dispose();
            response.StatusCode.Should().Be(201);
        }


        [Test]
        public async Task Returns400IfResidentContactParamModelStateIsInvalid()
        {
            var contactRequest = new ResidentContactParam
            {
                ContactValue = null
            };

            var url = new Uri($"/api/v1/contact-details", UriKind.Relative);
            using var content = new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, content).ConfigureAwait(true);
            response.StatusCode.Should().Be(400);
        }

    }
}
