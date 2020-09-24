using FluentAssertions;
using Moq;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class InsertExternalResidentRecordUseCaseTests
    {
        private Mock<IResidentGateway> _mockExternalResidentGateway;
        private InsertExternalResidentRecordUseCase _classUnderTest;
        [SetUp]
        public void Setup()
        {
            _mockExternalResidentGateway = new Mock<IResidentGateway>();
            _classUnderTest = new InsertExternalResidentRecordUseCase(_mockExternalResidentGateway.Object);
        }

        [Test]
        public void EnsureThatInsertExternalResidentRecordUseCaseCallsGateway()
        {
            var request = new InsertResidentRequest();
            _mockExternalResidentGateway.Setup(x => x.InsertNewResident(request)).Returns(new InsertResidentResponse());
            _classUnderTest.Execute(request);

            _mockExternalResidentGateway.Verify(x => x.InsertNewResident(request), Times.Once);
            _mockExternalResidentGateway.Verify(x => x.InsertExternalReferences(request, It.IsAny<int>()), Times.Once);
        }


        [Test]
        public void EnsureThatUseCaseReturnsTheIdReturnedByGateway()
        {
            var request = new InsertResidentRequest();
            var expectedId = 1;
            _mockExternalResidentGateway.Setup(x => x.InsertNewResident(request)).Returns(new InsertResidentResponse { ResidentId = expectedId, ResidentRecordAlreadyPresent = false });
            var result = _classUnderTest.Execute(request);

            result.ResidentId.Should().Be(expectedId);
            result.ResidentRecordAlreadyPresent.Should().BeFalse();
        }
    }
}
