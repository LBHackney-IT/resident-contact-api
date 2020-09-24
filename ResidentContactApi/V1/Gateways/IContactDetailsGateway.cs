using ContactDetailsDomain = ResidentContactApi.V1.Domain.ContactDetailsDomain;
using System.Collections.Generic;
using ResidentContactApi.V1.Domain;

namespace ResidentContactApi.V1.Gateways
{
    public interface IContactDetailsGateway
    {
      bool UpdateContactIsDefault(int contactId, bool value);

      ContactDetailsDomain GetContactById(int contactId);
    }
}
