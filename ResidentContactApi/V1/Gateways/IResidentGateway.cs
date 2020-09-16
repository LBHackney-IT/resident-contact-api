using ResidentDomain = ResidentContactApi.V1.Domain.ResidentDomain;
using System.Collections.Generic;
using ResidentContactApi.V1.Domain;

namespace ResidentContactApi.V1.Gateways
{
    public interface IResidentGateway
    {
        List<ResidentDomain> GetResidents(int limit, int cursor, string firstName, string lastName);
        ResidentDomain GetResidentById(int id);
        int? InsertResidentContactDetails(int? residentId, string nccContactId, ContactDetailsDomain contactDetails);
    }
}
