using System.Linq;
using AutoFixture;
using ResidentContactApi.V1.Boundary.Response;
using ContactDetailsResponse = ResidentContactApi.V1.Domain.ContactDetailsResponse;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Response.ContactDetails;

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class GetAllUseCaseTests
    {
        private Mock<IContactDetailsGateway> _mockGateway;
        private GetAllUseCase _classUnderTest;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IContactDetailsGateway>();
            _classUnderTest = new GetAllUseCase(_mockGateway.Object);
            _fixture = new Fixture();
        }

        //[Test]
        //public void GetsAllFromTheGateway()
        //{
        //    var stubbedEntities = _fixture.CreateMany<ContactDetailsResponse>().ToList();
        //    _mockGateway.Setup(x => x.GetAll()).Returns(stubbedEntities);

        //    var expectedResponse = new ContactDetailsResponseList { ContactDetails = stubbedEntities.ToResponse() };

        //    _classUnderTest.Execute().Should().BeEquivalentTo(expectedResponse);
        //}

        //TODO: Add extra tests here for extra functionality added to the use case
    }
}
