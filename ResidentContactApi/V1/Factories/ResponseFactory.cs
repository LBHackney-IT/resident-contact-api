using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Server.Kestrel.Core.Features;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Response.Residents;
using ResidentContactApi.V1.Domain;
using ContactDetailsDomain = ResidentContactApi.V1.Domain.ContactDetailsDomain;

namespace ResidentContactApi.V1.Factories
{
    public static class ResponseFactory
    {

        public static ResidentResponse ToResponse(this ResidentDomain domain)
        {
            return new ResidentResponse
            {
                Id = domain.Id,
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                DateOfBirth = domain.DateOfBirth,
                Gender = domain.Gender

            };
        }
        public static List<ResidentResponse> ToResponse(this IEnumerable<ResidentDomain> people)
        {
            return people.Select(p => p.ToResponse()).ToList();
        }

        private static ContactDetailsResponse ToResponse(this ContactDetailsDomain contactDetails)
        {
            return new ContactDetailsResponse
            {
                Id = contactDetails.Id,
                Type = contactDetails.Type,
                ContactValue = contactDetails.ContactValue,
                AddedBy = contactDetails.AddedBy,
                IsActive = contactDetails.IsActive,
                IsDefault = contactDetails.IsDefault,
                DateLastModified = contactDetails.DateLastModified,
                ModifiedBy = contactDetails.ModifiedBy,
                SubType = contactDetails.SubType

            };
        }

        public static List<ContactDetailsResponse> ToResponse(this IEnumerable<ContactDetailsDomain> contacts)
        {
            return contacts.Select(c => c.ToResponse()).ToList();
        }
    }
}
