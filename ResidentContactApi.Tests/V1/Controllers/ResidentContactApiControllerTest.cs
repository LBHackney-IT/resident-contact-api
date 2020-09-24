using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Controllers;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentContactApi.V1.Boundary;
using ResidentContactApi.V1.Boundary.Response;

namespace ResidentContactApi.Tests.V1.Controllers
{
    [TestFixture]
    public class ResidentContactApiControllerTest
    {
        private ResidentContactApiController _classUnderTest;

        private Mock<IGetAllUseCase> _mockGetAllUseCase;

        private Mock<IGetByIdUseCase> _mockGetByIdUseCase;

        private Mock<ICreateContactDetailsUseCase> _mockCreateContactDetails;
        private Mock<IInsertResidentRecordUseCase> _mockInsertResidentRecordUseCase;
        private Mock<IInsertExternalReferenceRecordUseCase> _mockInsertExternalReferenceRecordUseCase;


        [SetUp]
        public void SetUp()
        {
            _mockGetAllUseCase = new Mock<IGetAllUseCase>();
            _mockGetByIdUseCase = new Mock<IGetByIdUseCase>();
            _mockCreateContactDetails = new Mock<ICreateContactDetailsUseCase>();
            _mockInsertResidentRecordUseCase = new Mock<IInsertResidentRecordUseCase>();
            _mockInsertExternalReferenceRecordUseCase = new Mock<IInsertExternalReferenceRecordUseCase>();

            _classUnderTest = new ResidentContactApiController(_mockGetAllUseCase.Object, _mockGetByIdUseCase.Object,
                                                               _mockCreateContactDetails.Object, _mockInsertResidentRecordUseCase.Object,
                                                               _mockInsertExternalReferenceRecordUseCase.Object);
        }

        [Test]
        public void ListRecordsTest()
        {
            var residentInfo = new List<ResidentResponse>()
            {
                new ResidentResponse()
                {
                    Id = 1234,
                    FirstName = "test",
                    LastName = "test",
                    DateOfBirth = new DateTime()
                }
            };

            var residentInformationList = new ResidentResponseList()
            {
                Residents = residentInfo
            };

            var rqp = new ResidentQueryParam
            {
                FirstName = "Ciasom",
                LastName = "Tessellate",
            };

            _mockGetAllUseCase.Setup(x => x.Execute(rqp)).Returns(residentInformationList);
            var response = _classUnderTest.ListContacts(rqp) as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(residentInformationList);
        }

        [Test]

        public void ViewRecordTest()
        {
            var singleResidentInfo = new ResidentResponse()
            {
                Id = 1234,
                FirstName = "test",
                LastName = "test",
                DateOfBirth = new DateTime()
            };

            _mockGetByIdUseCase.Setup(x => x.Execute(1234)).Returns(singleResidentInfo);
            var response = _classUnderTest.ViewRecord(1234) as OkObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(200);
            response.Value.Should().BeEquivalentTo(singleResidentInfo);
        }

        [Test]
        public void ThrowsNotFoundError()
        {
            var id = 1234;
            _mockGetByIdUseCase.Setup(x => x.Execute(It.IsAny<int>())).Throws<ResidentNotFoundException>();
            var response = _classUnderTest.ViewRecord(id) as NotFoundObjectResult;

            response.Should().NotBeNull();
            response.StatusCode.Should().Be(404);

        }

        [Test]
        public void CreateRecordReturns201IfSuccessful()
        {
            var useCaseResponse = new ContactDetailsResponse();
            _mockCreateContactDetails.Setup(x => x.Execute(It.IsAny<ResidentContact>())).Returns(useCaseResponse);
            var result = _classUnderTest.CreateContactRecord(It.IsAny<ResidentContact>()) as CreatedAtActionResult;

            result.Should().NotBeNull();
            result.Value.Should().BeEquivalentTo(useCaseResponse);
            result.StatusCode.Should().Be(201);
        }

        [Test]
        public void CreateRecordReturns400IfNoIdExceptionThrown()
        {
            _mockCreateContactDetails.Setup(x => x.Execute(It.IsAny<ResidentContact>())).Throws<NoIdentifierException>();
            var result = _classUnderTest.CreateContactRecord(It.IsAny<ResidentContact>()) as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Test]
        public void CreateRecordReturns400IfResidentNotFoundExceptionThrown()
        {
            _mockCreateContactDetails.Setup(x => x.Execute(It.IsAny<ResidentContact>())).Throws<ResidentNotFoundException>();
            var result = _classUnderTest.CreateContactRecord(It.IsAny<ResidentContact>()) as BadRequestObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(400);
        }

        [Test]
        public void IfResidentToBeInsertedAlreadyExistsShouldReturn200StatusCode()
        {
            var response = new InsertResidentResponse { ResidentRecordAlreadyPresent = true };
            _mockInsertResidentRecordUseCase.Setup(x => x.Execute(It.IsAny<InsertResidentRequest>())).Returns(response);

            var result = _classUnderTest.InsertResident(It.IsAny<InsertResidentRequest>()) as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(response);
        }

        [Test]
        public void IfResidentToBeInsertedDoesNotAlreadyExistReturn201StatusCodeAndId()
        {
            var response = new InsertResidentResponse { ResidentId = 2, ResidentRecordAlreadyPresent = false };
            _mockInsertResidentRecordUseCase.Setup(x => x.Execute(It.IsAny<InsertResidentRequest>())).Returns(response);

            var result = _classUnderTest.InsertResident(It.IsAny<InsertResidentRequest>()) as CreatedAtActionResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(response);
        }
        [Test]
        public void InsertResidentReturns500StatusCodeIfResidentNotInsertExceptionIsRaised()
        {
            _mockInsertResidentRecordUseCase.Setup(x => x.Execute(It.IsAny<InsertResidentRequest>())).Throws(new ResidentNotInsertedException("error message"));
            var result = _classUnderTest.InsertResident(It.IsAny<InsertResidentRequest>()) as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Resident could not be inserted - error message");
        }
        [Test]
        public void InsertResidentReturns500StatusCodeIfExternalReferenceNotInsertExceptionIsRaised()
        {
            _mockInsertResidentRecordUseCase.Setup(x => x.Execute(It.IsAny<InsertResidentRequest>())).Throws(new ExternalReferenceNotInsertedException("error message"));
            var result = _classUnderTest.InsertResident(It.IsAny<InsertResidentRequest>()) as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("External reference could not be inserted - error message");
        }

        [Test]
        public void IfExternalReferenceToBeInsertedAlreadyExistsShouldReturn200StatusCode()
        {
            var response = new InsertResidentResponse { ResidentRecordAlreadyPresent = true };
            _mockInsertExternalReferenceRecordUseCase.Setup(x => x.Execute(It.IsAny<InsertResidentRequest>())).Returns(response);

            var result = _classUnderTest.InsertExternalResident(It.IsAny<InsertResidentRequest>()) as OkObjectResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(200);
            result.Value.Should().BeEquivalentTo(response);
        }

        [Test]
        public void IfExternalReferenceToBeInsertedDoesNotAlreadyExistReturn201StatusCodeAndId()
        {
            var response = new InsertResidentResponse { ResidentId = 2, ResidentRecordAlreadyPresent = false };
            _mockInsertExternalReferenceRecordUseCase.Setup(x => x.Execute(It.IsAny<InsertResidentRequest>())).Returns(response);

            var result = _classUnderTest.InsertExternalResident(It.IsAny<InsertResidentRequest>()) as CreatedAtActionResult;
            result.Should().NotBeNull();
            result.StatusCode.Should().Be(201);
            result.Value.Should().BeEquivalentTo(response);
        }
        [Test]
        public void InsertExternalReferenceReturns500StatusCodeIfResidentNotInsertExceptionIsRaised()
        {
            _mockInsertExternalReferenceRecordUseCase.Setup(x => x.Execute(It.IsAny<InsertResidentRequest>())).Throws(new ResidentNotInsertedException("error message"));
            var result = _classUnderTest.InsertExternalResident(It.IsAny<InsertResidentRequest>()) as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("Resident could not be inserted - error message");
        }
        [Test]
        public void InsertExternalReferenceReturns500StatusCodeIfExternalReferenceNotInsertExceptionIsRaised()
        {
            _mockInsertExternalReferenceRecordUseCase.Setup(x => x.Execute(It.IsAny<InsertResidentRequest>())).Throws(new ExternalReferenceNotInsertedException("error message"));
            var result = _classUnderTest.InsertExternalResident(It.IsAny<InsertResidentRequest>()) as ObjectResult;

            result.Should().NotBeNull();
            result.StatusCode.Should().Be(500);
            result.Value.Should().Be("External reference could not be inserted - error message");
        }
    }
}
