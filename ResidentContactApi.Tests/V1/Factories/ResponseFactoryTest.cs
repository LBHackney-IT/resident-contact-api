using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using ResidentContactApi.V1.Boundary.Response;
using FluentAssertions;
using ResidentContactApi.V1.Enums;

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
                Gender = GenderTypeEnum.F,
                Contacts = null

            };

            var expectedResponse = new ResidentResponse
            {
                Id = 1234,
                FirstName = "Name",
                LastName = "Last",
                DateOfBirth = new DateTime(),
                Gender = "F",
                Contacts = null
            };
            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void ReturnContactDetailsInResidentResponse()
        {
            var domain = new ResidentDomain
            {
                Id = 1234,
                FirstName = "Name",
                LastName = "Last",
                DateOfBirth = new DateTime(),
                Gender = GenderTypeEnum.F,
                Contacts = new List<ContactDetailsDomain>
                {
                   new ContactDetailsDomain
                   {
                       Id = 1234,
                       AddedBy = "Test"
                   }
                }
            };

            var expectedResponse = new ResidentResponse
            {
                Id = 1234,
                FirstName = "Name",
                LastName = "Last",
                DateOfBirth = new DateTime(),
                Gender = "F",
                Contacts = new List<ContactDetailsResponse>
                {
                    new ContactDetailsResponse
                    {
                        Id = 1234,
                        AddedBy = "Test"
                    }
                }
            };

            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }

        [Test]
        public void ReturnContactDetailsResponse()
        {
            var domain = new ContactDetailsDomain
            {
                Id = 1234,
                Type = "Address",
                ContactValue = "123456",
                AddedBy = "test",
                IsActive = false,
                IsDefault = false,
                DateLastModified = new DateTime(2020, 04, 23),
                ModifiedBy = "Tester",
                SubType = "Home",
                DateAdded = new DateTime(2021, 05, 21),
                ResidentId = 12345

            };
            var expectedResponse = new ContactDetailsResponse
            {
                Id = 1234,
                Type = "Address",
                ContactValue = "123456",
                AddedBy = "test",
                IsActive = false,
                IsDefault = false,
                DateLastModified = new DateTime(2020, 04, 23),
                ModifiedBy = "Tester",
                SubType = "Home",
                DateAdded = new DateTime(2021, 05, 21),
                ResidentId = 12345

            };
            domain.ToResponse().Should().BeEquivalentTo(expectedResponse);
        }
    }
}
