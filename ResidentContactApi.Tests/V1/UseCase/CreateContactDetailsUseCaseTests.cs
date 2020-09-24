using System;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using ResidentContactApi.V1.Boundary.Requests;
using AutoFixture;
using ResidentContactApi.V1.Boundary;

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class CreateContactDetailsUseCaseTests
    {
        private CreateContactDetailsUseCase _classUnderTest;
        private Mock<IResidentGateway> _mockGateway;
        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(2));
            _mockGateway = new Mock<IResidentGateway>();
            _classUnderTest = new CreateContactDetailsUseCase(_mockGateway.Object);
        }

        [Test]
        public void UseCaseShouldCallGatewayWithADomainObjectToInsertContactData()
        {
            var request = _fixture.Create<ResidentContact>();

            var expectedDomain = new ContactDetailsDomain
            {
                ContactValue = request.Value,
                IsActive = request.Active,
                IsDefault = request.Default,
                SubtypeId = request.SubtypeId,
                TypeId = request.TypeId,
                Id = request.Id
            };
            _mockGateway.Setup(
                x => x.InsertResidentContactDetails(request.ResidentId.Value, request.NccContactId,
                    It.Is<ContactDetailsDomain>(x => CheckContactDetails(x, expectedDomain)))).Returns(1);
            _classUnderTest.Execute(request);
            _mockGateway.Verify();

        }

        [Test]
        public void UseCaseShouldReturnTheIdOfTheResource()
        {
            var stubbedId = _fixture.Create<int>();

            _mockGateway.Setup(x => x.InsertResidentContactDetails(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<ContactDetailsDomain>()))
                .Returns(stubbedId);

            var response = _classUnderTest.Execute(_fixture.Create<ResidentContact>());

            response.Should().BeEquivalentTo(new ContactDetailsResponse
            {
                Id = stubbedId
            });
        }

        [Test]
        public void IfNoIdIsPresentExecuteShouldThrowNoIdException()
        {
            var request = _fixture.Build<ResidentContact>()
                .Without(c => c.ResidentId).Without(c => c.NccContactId).Create();
            Func<ContactDetailsResponse> testDelegate = () => _classUnderTest.Execute(request);
            testDelegate.Should().Throw<NoIdentifierException>();
        }

        [Test]
        public void IfGatewayReturnsNullExecuteShouldThrowResidentNotFound()
        {
            var request = _fixture.Create<ResidentContact>();
            _mockGateway.Setup(x => x.InsertResidentContactDetails(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<ContactDetailsDomain>()))
                .Returns((int?) null);
            Func<ContactDetailsResponse> testDelegate = () => _classUnderTest.Execute(request);
            testDelegate.Should().Throw<ResidentNotFoundException>();
        }

        private static bool CheckContactDetails(ContactDetailsDomain x, ContactDetailsDomain expectedDomain)
        {
            return x.ContactValue == expectedDomain.ContactValue
                   && x.IsActive == expectedDomain.IsActive
                   && x.IsDefault == expectedDomain.IsDefault
                   && x.SubtypeId == expectedDomain.SubtypeId
                   && x.TypeId == expectedDomain.TypeId
                   && x.Id == expectedDomain.Id;
        }
    }
}
