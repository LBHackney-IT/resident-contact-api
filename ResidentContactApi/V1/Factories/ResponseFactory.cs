using System.Collections.Generic;
using System.Linq;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Response.Residents;
using ResidentContactApi.V1.Domain;
using ContactDetailsDomain = ResidentContactApi.V1.Domain.ContactDetailsDomain;

namespace ResidentContactApi.V1.Factories
{
    public static class ResponseFactory
    {
        //TODO: Map the fields in the domain object(s) to fields in the response object(s).
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings

        public static ResidentResponse ToResponse(this ResidentDomain domain)
        {
            return new ResidentResponse
            {
                FirstName = domain.FirstName,
                LastName = domain.LastName,
                DateOfBirth = domain.DateOfBirth,
                Gender = domain.Gender,


            };
        }
        public static List<ResidentResponse> ToResponse(this IEnumerable<ResidentDomain> people)
        {
            return people.Select(p => p.ToResponse()).ToList();
        }

        private static List<ContactDetailsResponse> ToResponse(this List<Domain.ContactDetailsDomain> contactDetails)
        {
            return contactDetails.Select(contact => new ContactDetailsResponse
            {
                Id = contact.Id,
                ContactType = contact.ContactType,
                ContactValue = contact.ContactValue,
                AddedBy = contact.AddedBy,
                IsActive = contact.IsActive,
                IsDefault = contact.IsDefault,
                DateLastModified = contact.DateLastModified,
                ModifiedBy = contact.ModifiedBy

            }).ToList();
        }
    }
}
