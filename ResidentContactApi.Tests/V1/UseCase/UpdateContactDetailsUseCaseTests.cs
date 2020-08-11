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

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class UpdateContactDetailsUseCaseTests
    {
        private UpdateContactDetailsUseCase _classUnderTest;
        private Mock<IResidentGateway> _mockGateway;
        private Faker _faker;
        [SetUp]
        public void Setup()
        {
            _mockGateway = new Mock<IResidentGateway>();
            _classUnderTest = new UpdateContactDetailsUseCase(_mockGateway.Object);
            _faker = new Faker();
        }

        [Test]
        [Ignore("")]
        public void UseCaseShouldCallGatewayToInsertContactData()
        {

        }

        [Test]
        [Ignore("")]
        public void UseCaseShouldCallGatewayToUpdateContactData()
        {

        }
    }
}
