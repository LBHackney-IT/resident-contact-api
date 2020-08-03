using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using ResidentContactApi.V1.Boundary.Response.Residents;
using ResidentContactApi.V1.Boundary.Response;
using FluentAssertions;

namespace ResidentContactApi.Tests.V1.Factories
{
    public class ResponseFactoryTest
    {
        [Test]
        public void CanMapResidentInformationFromDomainToResponse()
        {
            var domain = new ResidentDomain
            {

                Id = 1234,
                FirstName = "Name",
                LastName = "Last",
                DateOfBirth = new DateTime(),
                Gender = "Female",
                Contacts = null

            };

            var expectedResponse = new ResidentResponse
            {
                Id = 1234,
                FirstName = "Name",
                LastName = "Last",
                DateOfBirth = new DateTime(),
                Gender = "Female",
                Contacts = null
            };
            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void CanMapResidentInformationWithOnlyPersonalInformationFromDomainToResponse()
        {
            var domain = new ResidentDomain
            {
                Id = 1234,
                FirstName = "Name",
                LastName = "Last",
                DateOfBirth = new DateTime(),
                Gender = "Male",
                Contacts = null
            };


            var expectedResponse = new ResidentResponse
            {
                Id = 1234,
                FirstName = "Name",
                LastName = "Last",
                DateOfBirth = new DateTime(),
                Gender = "Male",
                Contacts = null

            };
            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }
    }
}
