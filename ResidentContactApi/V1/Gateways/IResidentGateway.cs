using ResidentContactApi.V1.Boundary.Requests;
using ResidentDomain = ResidentContactApi.V1.Domain.ResidentDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Gateways
{
    public interface IResidentGateway
    {
        List<ResidentDomain> GetResidents(int limit, int cursor, string firstName, string lastName);
        ResidentDomain GetResidentById(int id);
        ResidentDomain InsertResidentContactDetails(ResidentContact rcp);

    }
}
