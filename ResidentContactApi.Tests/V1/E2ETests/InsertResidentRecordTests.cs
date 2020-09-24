using Bogus;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ResidentContactApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class InsertResidentRecordTests : IntegrationTests<Startup>
    {
        private readonly Faker _faker = new Faker();

        [Test]
        public async Task ShouldReturn400IfRequestParametersAreMissing()
        {
            var request = new InsertResidentRequest
            {
                FirstName = _faker.Random.Word()
            };

            var url = new Uri("/api/v1/residents", UriKind.Relative);
            using var requestContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, requestContent).ConfigureAwait(true);
            response.StatusCode.Should().Be(400);
        }
        [Test]
        public async Task ShouldReturn400IfNoExternalReferencesAreSupplied()
        {
            var request = new InsertResidentRequest
            {
                FirstName = _faker.Random.Word(),
                LastName = _faker.Random.Word(),
                DateOfBirth = _faker.Date.Past()
            };

            var url = new Uri("/api/v1/residents", UriKind.Relative);
            using var requestContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, requestContent).ConfigureAwait(true);
            response.StatusCode.Should().Be(400);
        }

        [Test]
        public async Task ShouldReturn201WhenNewResidentRecordIsInserted()
        {
            var externalSystemLookup = new ExternalSystemLookup
            {
                Name = _faker.Random.Word()
            };
            ResidentContactContext.ExternalSystemLookups.Add(externalSystemLookup);
            ResidentContactContext.SaveChanges();

            var request = new InsertResidentRequest
            {
                FirstName = _faker.Random.Word(),
                LastName = _faker.Random.Word(),
                DateOfBirth = _faker.Date.Past(),
                ExternalReferences = new List<InsertExternalReferenceRequest> {
                    new InsertExternalReferenceRequest {
                        ExternalReferenceValue = _faker.Random.Word(),
                        ExternalReferenceName = _faker.Random.Word(),
                        ExternalSystemId = externalSystemLookup.Id
                    }
                 }
            };

            var url = new Uri("/api/v1/residents", UriKind.Relative);
            using var requestContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, requestContent).ConfigureAwait(true);
            var content = response.Content.ReadAsStringAsync().Result;
            var insertResidentResponse = JsonConvert.DeserializeObject<InsertResidentResponse>(content);

            response.StatusCode.Should().Be(201);
            CheckResidentHasBeenInserted(insertResidentResponse.ResidentId, request);
        }


        [Test]
        public async Task ShouldReturn200IfResidentIsAlreadyPresentInDatabase()
        {

            var residentInDb = E2ETestsHelper.AddResidentRecordToTheDatabase(ResidentContactContext);
            var crmReference = E2ETestsHelper.AddCrmContactIdForResidentId(ResidentContactContext, residentInDb.Id);

            var request = new InsertResidentRequest
            {
                FirstName = _faker.Random.Word(),
                LastName = _faker.Random.Word(),
                DateOfBirth = _faker.Date.Past(),
                ExternalReferences = new List<InsertExternalReferenceRequest> {
                    new InsertExternalReferenceRequest {
                        ExternalReferenceValue = crmReference.ExternalIdValue,
                        ExternalReferenceName = "ContactId",
                        ExternalSystemId = crmReference.ExternalSystemLookupId
                    }
                 }
            };

            var url = new Uri("/api/v1/residents", UriKind.Relative);
            using var requestContent =
                new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            var response = await Client.PostAsync(url, requestContent).ConfigureAwait(true);
            var content = response.Content.ReadAsStringAsync().Result;
            var insertResidentResponse = JsonConvert.DeserializeObject<InsertResidentResponse>(content);

            response.StatusCode.Should().Be(200);
            insertResidentResponse.ResidentRecordAlreadyPresent.Should().BeTrue();
        }

        private void CheckResidentHasBeenInserted(int residentId, InsertResidentRequest request)
        {
            var resident = ResidentContactContext.Residents.Where(x => x.Id == residentId).FirstOrDefault();
            resident.FirstName.Should().Be(request.FirstName);
            resident.LastName.Should().Be(request.LastName);
            resident.Gender.Should().Be(request.Gender);
            resident.DateOfBirth.Should().Be(request.DateOfBirth);

            var externalReferences = ResidentContactContext.ExternalSystemIds.Where(x => x.ResidentId == residentId).ToList();
            externalReferences.Count.Should().Be(request.ExternalReferences.Count);
        }

    }
}
