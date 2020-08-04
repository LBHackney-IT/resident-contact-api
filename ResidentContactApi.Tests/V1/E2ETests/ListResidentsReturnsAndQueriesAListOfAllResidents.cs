using AutoFixture;
using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Response.Residents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.Tests.V1.E2ETests
{
    public class ListResidentsReturnsAndQueriesAListOfAllResidents : IntegrationTests<Startup>
    {
        private IFixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
        }

        [Test]
        public async Task IfNoQueryParametersReturnsAllResidentRecords()
        {
            var expectedResidentResponseOne = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext);
            var expectedResidentResponseTwo = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext);
            var expectedResidentResponseThree = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext);

            var uri = new Uri("api/v1/contacts", UriKind.Relative);
            var response = Client.GetAsync(uri);

            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentResponseList>(stringContent);

            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseOne);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseTwo);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseThree);
        }

        [Test]
        public async Task FirstNameLastNameQueryParametersReturnsMatchingResidentRecords()
        {
            var expectedResidentResponseOne = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext, firstname: "ciasom", lastname: "tessellate");
            var expectedResidentResponseTwo = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext, firstname: "ciasom", lastname: "shape");
            var expectedResidentResponseThree = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext);

            var uri = new Uri("api/v1/contacts?firstName=ciasom&lastName=tessellate", UriKind.Relative);
            var response = Client.GetAsync(uri);

            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentResponseList>(stringContent);

            convertedResponse.Residents.Count.Should().Be(1);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseOne);
        }
        [Test]
        public async Task FirstNameLastNameQueryParametersWildcardSearchReturnsMatchingResidentRecords()
        {
            var expectedResidentResponseOne = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext, firstname: "ciasom", lastname: "tessellate");
            var expectedResidentResponseTwo = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext, firstname: "ciasom", lastname: "shape");
            var expectedResidentResponseThree = E2ETestsHelper.AddPersonWithRelatedEntitiestoDb(ResidentContactContext);

            var uri = new Uri("api/v1/contacts?firstName=ciaso&lastName=sell", UriKind.Relative);
            var response = Client.GetAsync(uri);

            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var content = response.Result.Content;
            var stringContent = await content.ReadAsStringAsync().ConfigureAwait(true);
            var convertedResponse = JsonConvert.DeserializeObject<ResidentResponseList>(stringContent);

            convertedResponse.Residents.Count.Should().Be(1);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseOne);
        }


    }

}
