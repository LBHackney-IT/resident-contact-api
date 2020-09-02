using System.Collections.Generic;
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
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Test]
        public void ReturnsResidentInformationList()
        {
            var stubbedResidents = _fixture
                .Build<ResidentDomain>()
                .Without(contact => contact.Contacts)
                .CreateMany();

            SetupMockGatewayToExpectParameters(firstNameQuery: "ciasom", lastNameQuery: "tessellate",
                stubbedResidents: stubbedResidents.ToList());

            var rqp = new ResidentQueryParam
            {
                FirstName = "ciasom",
                LastName = "tessellate"
            };

            var response = _classUnderTest.Execute(rqp);

            response.Should().NotBeNull();
            response.Residents.Should().BeEquivalentTo(stubbedResidents.ToResponse());
        }

        [Test]
        public void ExecuteCallsTheGatewayWithLimitAndCursor()
        {
            SetupMockGatewayToExpectParameters(limit: 23, cursor: 236712);
            CallUseCaseWithArgs(23, 236712);
            _mockGateway.Verify();
        }

        [Test]
        public void ExecuteIfNoCursorSuppliedPasses0ToTheGateway()
        {
            SetupMockGatewayToExpectParameters(cursor: 0);
            CallUseCaseWithArgs(20, 0);
            _mockGateway.Verify();
        }

        [Test]
        public void IfLimitLessThanTheMinimumWillUseTheMinimumLimit()
        {
            SetupMockGatewayToExpectParameters(limit: 10);
            CallUseCaseWithArgs(0, 0);
            _mockGateway.Verify();
        }

        [Test]
        public void IfLimitMoreThanTheMaximumWillUseTheMaximumLimit()
        {
            SetupMockGatewayToExpectParameters(limit: 100);
            CallUseCaseWithArgs(400, 0);
            _mockGateway.Verify();
        }

        [Test]
        public void ReturnsTheNextCursor()
        {
            var stubbedTenancies = _fixture.CreateMany<ResidentDomain>(10)
                .OrderBy(r => r.Id).ToList();
            SetupMockGatewayToExpectParameters(limit: 10, stubbedResidents: stubbedTenancies);

            var expectedNextCursor = stubbedTenancies.Last().Id.ToString();

            CallUseCaseWithArgs(10, 0).NextCursor.Should().Be(expectedNextCursor);
        }

        [Test]
        public void WhenAtTheEndOfTheResidentListReturnsNullForTheNextCursor()
        {
            var stubbedResidents = _fixture.CreateMany<ResidentDomain>(7);
            SetupMockGatewayToExpectParameters(limit: 10, stubbedResidents: stubbedResidents);

            CallUseCaseWithArgs(10, 0).NextCursor.Should().Be(null);
        }

        private void SetupMockGatewayToExpectParameters(int? limit = null, int? cursor = null,
            string firstNameQuery = null, string lastNameQuery = null, IEnumerable<ResidentDomain> stubbedResidents = null)
        {
            _mockGateway
                .Setup(x =>
                    x.GetResidents(It.Is<int>(l => CheckParameter(limit, l)), cursor ?? It.IsAny<int>(), firstNameQuery, lastNameQuery))
                .Returns(stubbedResidents?.ToList() ?? new List<ResidentDomain>()).Verifiable();
        }

        private static bool CheckParameter(int? expectedParam, int receivedParam)
        {
            return expectedParam == null || receivedParam == expectedParam.Value;
        }

        private ResidentResponseList CallUseCaseWithArgs(int limit = 20, int cursor = 0, string firstName = null, string lastName = null)
        {
            return _classUnderTest.Execute(new ResidentQueryParam { Limit = limit, Cursor = cursor, FirstName = firstName, LastName = lastName });
        }
    }
}
