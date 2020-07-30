using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response.Residents;
using ResidentContactApi.V1.Controllers;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.Tests.V1.Controllers
{
    [TestFixture]
    public class ResidentContactApiControllerTest
    {
        private ResidentContactApiController _classUnderTest;

        private Mock<IGetAllUseCase> _mockGetAllUseCase;

        private Mock<IGetByIdUseCase> _mockGetByIdUseCase;


        [SetUp]
        public void SetUp()
        {
            _mockGetAllUseCase = new Mock<IGetAllUseCase>();
            _mockGetByIdUseCase = new Mock<IGetByIdUseCase>();
            _classUnderTest = new ResidentContactApiController(_mockGetAllUseCase.Object, _mockGetByIdUseCase.Object);
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
    }

}
