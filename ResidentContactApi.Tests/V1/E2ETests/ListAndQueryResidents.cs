using FluentAssertions;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using ResidentContactApi.V1.Boundary.Response;

namespace ResidentContactApi.Tests.V1.E2ETests
{
    public class ListAndQueryResidents : IntegrationTests<Startup>
    {
        [Test]
        public async Task IfNoQueryParametersReturnsAllResidentRecords()
        {
            var expectedResidentResponseOne = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext);
            var expectedResidentResponseTwo = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext);
            var expectedResidentResponseThree = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext);

            var response = CallEndpointWithQueryString();

            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var convertedResponse = await DeserializeResponse(response.Result).ConfigureAwait(true);

            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseOne);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseTwo);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseThree);
        }

        [Test]
        public async Task FirstNameLastNameQueryParametersReturnsMatchingResidentRecords()
        {
            var expectedResidentResponseOne = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext, firstname: "ciasom", lastname: "tessellate");
            var expectedResidentResponseTwo = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext, firstname: "ciasom", lastname: "shape");
            var expectedResidentResponseThree = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext);

            var response = CallEndpointWithQueryString("?firstName=ciasom&lastName=tessellate");

            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var convertedResponse = await DeserializeResponse(response.Result).ConfigureAwait(true);


            convertedResponse.Residents.Count.Should().Be(1);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseOne);
        }

        [Test]
        public async Task FirstNameLastNameQueryParametersWildcardSearchReturnsMatchingResidentRecords()
        {
            var expectedResidentResponseOne = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext, firstname: "ciasom", lastname: "tessellate");
            var expectedResidentResponseTwo = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext, firstname: "ciasom", lastname: "shape");
            var expectedResidentResponseThree = E2ETestsHelper.AddPersonWithRelatedEntitiesToDb(ResidentContactContext);

            var response = CallEndpointWithQueryString("?firstName=ciaso&lastName=sell");
            var statusCode = response.Result.StatusCode;
            statusCode.Should().Be(200);

            var convertedResponse = await DeserializeResponse(response.Result).ConfigureAwait(true);

            convertedResponse.Residents.Count.Should().Be(1);
            convertedResponse.Residents.Should().ContainEquivalentOf(expectedResidentResponseOne);
        }

        [Test]
        public async Task WithNoCursorAndLimitWillReturnTheFirstPageOfResidentsWithTheNextCursor()
        {
            var allSavedResidents = Enumerable.Range(0, 25)
                .Select(r =>
                    E2ETestsHelper
                        .AddPersonWithRelatedEntitiesToDb(ResidentContactContext, r + 1))
                .ToList();

            var response = await CallEndpointWithQueryString().ConfigureAwait(true);
            response.StatusCode.Should().Be(200);

            var returnedResidents = await DeserializeResponse(response).ConfigureAwait(true);
            returnedResidents.Residents.Should().BeEquivalentTo(allSavedResidents.Take(20));

            returnedResidents.NextCursor.Should().Be("19");
        }

        [Test]
        public async Task WithCursorAndLimitGivenWillCorrectlyPaginateResidentsReturned()
        {
            var allSavedEntities = Enumerable.Range(0, 17)
                .Select(r =>
                    E2ETestsHelper
                        .AddPersonWithRelatedEntitiesToDb(ResidentContactContext, r + 1))
                .ToList();

            var response = await CallEndpointWithQueryString("?limit=12&cursor=2").ConfigureAwait(true);
            response.StatusCode.Should().Be(200);

            var returnedTenancies = await DeserializeResponse(response).ConfigureAwait(true);
            returnedTenancies.Residents.Should().BeEquivalentTo(allSavedEntities.Skip(3).Take(12));
        }

        private async Task<HttpResponseMessage> CallEndpointWithQueryString(string query = null)
        {
            var uri = new Uri($"api/v1/contacts{query}", UriKind.Relative);
            return await Client.GetAsync(uri).ConfigureAwait(true);
        }

        private static async Task<ResidentResponseList> DeserializeResponse(HttpResponseMessage response)
        {
            var stringContent = await response.Content.ReadAsStringAsync().ConfigureAwait(true);
            return JsonConvert.DeserializeObject<ResidentResponseList>(stringContent);
        }
    }
}
