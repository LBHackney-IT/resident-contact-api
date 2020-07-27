using System.Linq;
using AutoFixture;
using ResidentContactApi.V1.Boundary.Response;
using ContactDetailsResponse = ResidentContactApi.V1.Domain.ContactDetailsDomain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Response.ContactDetails;
using ResidentContactApi.V1.Boundary.Response.Residents;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Boundary.Requests;

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests
    {
        private Mock<IResidentGateway> _mockGateway;
        private GetAllUseCase _classUnderTest;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IResidentGateway>();
            _classUnderTest = new GetAllUseCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        [Test]
        public void ReturnsResidentInformationList()
        {
            var stubbedResidents = _fixture.CreateMany<ResidentDomain>();

            _mockGateway.Setup(x =>
                    x.GetResidents("ciasom", "tessellate"))
                .Returns(stubbedResidents.ToList());
            var rqp = new ResidentQueryParam
            {
                FirstName = "ciasom",
                LastName = "tessellate"
            };

            var response = _classUnderTest.Execute(rqp);

            response.Should().NotBeNull();
            response.Residents.Should().BeEquivalentTo(stubbedResidents.ToResponse());
        }
    }
}
