using ResidentContactApi.V1.Boundary;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase;
using Bogus;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentContactApi.V1.Boundary.Requests;
using AutoFixture;
using ResidentContactApi.V1.Factories;

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class CreateContactDetailsUseCaseTests
    {
        private CreateContactDetailsUseCase _classUnderTest;
        private Mock<IResidentGateway> _mockGateway;
        private static Faker _faker = new Faker();
        private static Fixture _fixture = new Fixture();

        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<IResidentGateway>();
            _classUnderTest = new CreateContactDetailsUseCase(_mockGateway.Object);
        }

        [Test]
        public void UseCaseShouldCallGatewayToInsertContactData()
        {
            var request = GetResidentContactParameter();

            var stubbedContactInfo = _fixture
               .Build<ResidentDomain>()
               .Without(contact => contact.Contacts)
               .Create();
            stubbedContactInfo.Contacts = _fixture
                .Build<ContactDetailsDomain>()
                .Without(resident => resident.Resident)
                .CreateMany().ToList();

            _mockGateway.Setup(x => x.InsertResidentContactDetails(request)).Returns(stubbedContactInfo);

            var response = _classUnderTest.Execute(request);
            var expectedContact = stubbedContactInfo.ToResponse();

            response.Should().NotBeNull();
            response.Should().BeOfType<ResidentResponse>();
        }

        private static ResidentContact GetResidentContactParameter()
        {
            return new ResidentContact
            {
                SubtypeId = _faker.Random.Int(1, 5),
                TypeId = _faker.Random.Int(1, 5),
                Value = _faker.Random.String(11, 100),
                Active = _faker.Random.Bool(),
                Default = _faker.Random.Bool(),
                ResidentId = _faker.Random.Int(1)
            };
        }
    }
}
