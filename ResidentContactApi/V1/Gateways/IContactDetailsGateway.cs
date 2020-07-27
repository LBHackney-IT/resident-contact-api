using System.Collections.Generic;
using System.Threading.Tasks;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;

namespace ResidentContactApi.V1.Gateways
{
    public interface IContactDetailsGateway
    {
        List<ContactDetailsDomain> GetContactDetails(ResidentQueryParam rqp);
    }
}
