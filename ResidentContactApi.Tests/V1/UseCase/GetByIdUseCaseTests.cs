using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase;
using Moq;
using NUnit.Framework;
using ResidentContactApi.V1.Domain;
using FluentAssertions;
using AutoFixture;
using ResidentContactApi.V1.Factories;
using System;
using ResidentContactApi.V1.Boundary.Response.Residents;
using System.Linq;
using System.Collections.Generic;

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class GetByIdUseCaseTests
    {
        private Mock<IResidentGateway> _mockGateway;
        private GetByIdUseCase _classUnderTest;
        private readonly Fixture _fixture = new Fixture();


        [SetUp]
        public void SetUp()
        {
            _mockGateway = new Mock<IResidentGateway>();
            _classUnderTest = new GetByIdUseCase(_mockGateway.Object);
        }

        [Test]
        public void ReturnResidentInformationRecordWithContactForSpecifiedId()
        {
            var stubbedResidentInfo = _fixture
                .Build<ResidentDomain>()
                .Without(contact => contact.Contacts)
                .Create();
            stubbedResidentInfo.Contacts = _fixture
                .Build<ContactDetailsDomain>()
                .Without(resident => resident.Resident)
                .CreateMany().ToList();

            var id = _fixture.Create<int>();

            _mockGateway.Setup(x => x.GetResidentById(id)).Returns(stubbedResidentInfo);

            var response = _classUnderTest.Execute(id);
            var expectedResponse = stubbedResidentInfo.ToResponse();

            response.Contacts.Should().BeEquivalentTo(expectedResponse.Contacts);
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);

        }

        [Test]

        public void ReturnResidentInformationRecordWithoutContactForSpecifiedId()
        {
            var stubbedResidentInfo = _fixture
                .Build<ResidentDomain>()
                .Without(contact => contact.Contacts)
                .Create();

            var id = _fixture.Create<int>();

            stubbedResidentInfo.Contacts = null;

            _mockGateway.Setup(x => x.GetResidentById(id)).Returns(stubbedResidentInfo);

            var response = _classUnderTest.Execute(id);
            var expectedResponse = stubbedResidentInfo.ToResponse();

            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void IfGatewayReturnsNullThrowNotFoundException()
        {
            ResidentDomain resultFromGateway = null;

            _mockGateway.Setup(x => x.GetResidentById(It.IsAny<int>())).Returns(resultFromGateway);

            Func<ResidentResponse> testDelegate = () => _classUnderTest.Execute(1234);
            testDelegate.Should().Throw<ResidentNotFoundException>();
        }
    }
}
