using System;
using System.Collections.Generic;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Linq;
using AutoFixture;

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class UpdateContactDetailsIsDefaultUseCaseTests
    {
        private UpdateContactDetailsIsDefaultUseCase _classUnderTest;
        private Mock<IContactDetailsGateway> _mockContactDetailsGateway;
        private Mock<IResidentGateway> _mockResidentGateway;
        private readonly Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(2));
            _mockContactDetailsGateway = new Mock<IContactDetailsGateway>();
            _mockResidentGateway = new Mock<IResidentGateway>();
            _classUnderTest = new UpdateContactDetailsIsDefaultUseCase(_mockContactDetailsGateway.Object, _mockResidentGateway.Object);
        }

        [Test]
        public void InvalidIDShouldThrowContactNotFoundException()
        {
            var request = _fixture.Create<ContactDetails>();
            var stubbedId = _fixture.Create<int>();
            Func<bool> testDelegate = () => _classUnderTest.Execute(stubbedId, request);
            testDelegate.Should().Throw<ContactNotFoundException>();
        }

        [Test]
        public void ChangeSingleContactForResidentShouldCallGetContactById()
        {
            var request = _fixture.Create<ContactDetails>();
            var expectedDomain = _fixture.Create<ContactDetailsDomain>();
            var stubbedId = _fixture.Create<int>();

            _mockContactDetailsGateway.Setup(x => x.GetContactById(stubbedId)).Returns(expectedDomain).Verifiable();
            _mockContactDetailsGateway.Setup(x => x.UpdateContactIsDefault(stubbedId, It.IsAny<bool>())).Returns(true).Verifiable();
            _classUnderTest.Execute(stubbedId, request);
            _mockContactDetailsGateway.Verify();
        }

        [Test]
        public void ChangeSingleContactForResidentShouldCallUpdateContactIsDefault()
        {
            var request = _fixture.Create<ContactDetails>();
            var expectedDomain = _fixture.Create<ContactDetailsDomain>();
            var stubbedId = _fixture.Create<int>();

            _mockContactDetailsGateway.Setup(x => x.GetContactById(stubbedId)).Returns(expectedDomain);
            _mockContactDetailsGateway.Setup(x => x.UpdateContactIsDefault(stubbedId, It.IsAny<bool>())).Returns(true).Verifiable();
            _classUnderTest.Execute(stubbedId, request);
            _mockContactDetailsGateway.Verify();
        }

        [Test]
        public void ChangeSingleContactForResidentWithMultipleContactsShouldNotLoadAllContactsOnFalseChange()
        {
            var request = _fixture.Create<ContactDetails>();
            var expectedDomain = _fixture.Create<ContactDetailsDomain>();
            var stubbedId = _fixture.Create<int>();
            var stubbedResidentId = _fixture.Create<int>();

            var expectedResidentDomain = new ResidentDomain
            {
                Id = stubbedResidentId,
                Contacts = new List<ContactDetailsDomain> { expectedDomain },

            };

            _mockContactDetailsGateway.Setup(x => x.GetContactById(stubbedId)).Returns(expectedDomain);
            _mockContactDetailsGateway.Setup(x => x.UpdateContactIsDefault(stubbedId, It.IsAny<bool>())).Returns(true);
            _mockResidentGateway.Setup(x => x.GetResidentById(stubbedResidentId)).Returns(expectedResidentDomain);
            _classUnderTest.Execute(stubbedId, request);
            _mockContactDetailsGateway.Verify();
        }
    }
}
