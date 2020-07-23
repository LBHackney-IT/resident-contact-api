using System.Collections.Generic;
using System.Linq;
using ResidentContactApi.V1.Boundary.Response;
using ContactDetailsDomain = ResidentContactApi.V1.Domain.ContactDetailsDomain;

namespace ResidentContactApi.V1.Factories
{
    public static class ResponseFactory
    {
        //TODO: Map the fields in the domain object(s) to fields in the response object(s).
        // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings
        public static ContactDetailsDomain ToResponse(this ContactDetailsDomain domain)
        {
            return new ContactDetailsDomain();
        }

        public static List<ContactDetailsDomain> ToResponse(this IEnumerable<ContactDetailsDomain> domainList)
        {
            return domainList.Select(domain => domain.ToResponse()).ToList();
        }
    }
}
