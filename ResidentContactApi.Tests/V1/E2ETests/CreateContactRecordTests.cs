using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Requests;
using Bogus;
using System.Text;
using System.Net.Http;
using ResidentContactApi.V1.Infrastructure;

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


            var resident = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext,
                contactTypeLookupId: contactRequest.TypeId,
                contactSubTypeLookupId: contactRequest.SubtypeId);

            contactRequest.ResidentId = resident.Id;
            var response = await CallPostEndpointWithRequest(contactRequest).ConfigureAwait(true);

            response.StatusCode.Should().Be(201);
            CheckContactHasBeSavedInDatabaseForResidentId(resident.Id, contactRequest);
        }


        [Test]
        public async Task Returns400IfResidentContactParamModelStateIsInvalid()
        {
            var contactRequest = new ResidentContact
            {
                Value = null
            };

            var response = await CallPostEndpointWithRequest(contactRequest).ConfigureAwait(true);
            response.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task CanCreateANewContactDetailGivenAnExternalContactIdToIdentifyTheResident()
        {
            var contactRequest = new ResidentContact
            {
                SubtypeId = _faker.Random.Int(1, 50),
                TypeId = _faker.Random.Int(1, 50),
                Value = "test@test",
                Active = _faker.Random.Bool(),
                Default = _faker.Random.Bool()
            };


            var resident = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext,
                contactTypeLookupId: contactRequest.TypeId,
                contactSubTypeLookupId: contactRequest.SubtypeId);
            contactRequest.NccContactId = E2ETestsHelper.AddCrmContactIdForResidentId(ResidentContactContext, resident.Id).ExternalIdValue;

            var response = await CallPostEndpointWithRequest(contactRequest).ConfigureAwait(true);

            response.StatusCode.Should().Be(201);
            CheckContactHasBeSavedInDatabaseForResidentId(resident.Id, contactRequest);
        }

        private void CheckContactHasBeSavedInDatabaseForResidentId(int residentId, ResidentContact contactRequest)
        {
            var savedContact = ResidentContactContext.ContactDetails
                .Where(c => c.ResidentId == residentId)
                .FirstOrDefault(c => c.ContactValue == contactRequest.Value);
            savedContact.Should().NotBeNull();
            savedContact.ContactValue.Should().BeEquivalentTo(contactRequest.Value);
            savedContact.ContactSubTypeLookupId.Should().Be(contactRequest.SubtypeId);
            savedContact.ContactTypeLookupId.Should().Be(contactRequest.TypeId);
            savedContact.IsActive.Should().Be(contactRequest.Active);
            savedContact.IsDefault.Should().Be(contactRequest.Default);
        }

        private async Task<HttpResponseMessage> CallPostEndpointWithRequest(ResidentContact contactRequest)
        {
            var url = new Uri("/api/v1/contact-details", UriKind.Relative);
            using var content =
                new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            using var response = await Client.PostAsync(url, content).ConfigureAwait(true);
            return response;
        }
    }
}
