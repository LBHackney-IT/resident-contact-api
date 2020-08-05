using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using ResidentContactApi.V1.Boundary.Response;

namespace ResidentContactApi.Tests.V1.E2ETests
{
    [TestFixture]
    public class GetResidentById : IntegrationTests<Startup>
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetResidentInformationByIdReturn200()
        {
            var residentId = _fixture.Create<int>();
            var expectedResponse = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext, residentId);
            var uri = new Uri($"api/v1/contacts/{residentId}", UriKind.Relative);
            var response = Client.GetAsync(uri);
            var statuscode = response.Result.StatusCode;
            statuscode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentResponse>(stringContent);

            convertedResponse.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void GetResidentByIdReturns404IfNotFound()
        {
            var uri = new Uri($"api/v1/contacts/132", UriKind.Relative);
            var response = Client.GetAsync(uri);
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(404);
        }
    }
}
