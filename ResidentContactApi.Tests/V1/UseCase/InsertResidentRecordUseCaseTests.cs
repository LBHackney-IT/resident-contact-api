using FluentAssertions;
using Moq;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase;
using ResidentContactApi.V1.Boundary.Response;

namespace ResidentContactApi.Tests.V1.UseCase
{
    public class InsertResidentRecordUseCaseTests
    {
        private Mock<IResidentGateway> _mockResidentGateway;
        private InsertResidentRecordUseCase _classUnderTest;
        [SetUp]
        public void Setup()
        {
            _mockResidentGateway = new Mock<IResidentGateway>();
            _classUnderTest = new InsertResidentRecordUseCase(_mockResidentGateway.Object);
        }

        [Test]
        public void EnsureThatInsertResidentRecordUseCaseCallsGateway()
        {
            var request = new InsertResidentRequest();
            _mockResidentGateway.Setup(x => x.InsertNewResident(request)).Returns(new InsertResidentResponse());
            _classUnderTest.Execute(request);

            _mockResidentGateway.Verify(x => x.InsertNewResident(request), Times.Once);
            _mockResidentGateway.Verify(x => x.InsertExternalReferences(request, It.IsAny<int>()), Times.Once);
        }


        [Test]
        public void EnsureThatUseCaseReturnsTheIdReturnedByGateway()
        {
            var request = new InsertResidentRequest();
            var expectedId = 1;
            _mockResidentGateway.Setup(x => x.InsertNewResident(request)).Returns(new InsertResidentResponse { ResidentId = expectedId, ResidentRecordAlreadyPresent = false });
            var result = _classUnderTest.Execute(request);

            result.ResidentId.Should().Be(expectedId);
            result.ResidentRecordAlreadyPresent.Should().BeFalse();
        }
    }
}
