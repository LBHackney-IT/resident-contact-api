using System.Collections.Generic;
using System.Linq;
using ResidentContactApi.V1.Boundary.Response;
using ContactDetailsResponse = ResidentContactApi.V1.Domain.ContactDetailsResponse;

namespace ResidentContactApi.V1.Factories
{
    public static class ResponseFactory
    {
        //TODO: Map the fields in the domain object(s) to fields in the response object(s).
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings
        public static ContactDetailsResponse ToResponse(this ContactDetailsResponse domain)
        {
            return new ContactDetailsResponse();
        }

        public static List<ContactDetailsResponse> ToResponse(this IEnumerable<ContactDetailsResponse> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
