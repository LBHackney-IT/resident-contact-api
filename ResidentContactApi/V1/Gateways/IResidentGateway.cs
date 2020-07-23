using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Gateways
{
    public interface IResidentGateway
    {
        Task<List<ResidentResponse>> GetResidents(ResidentQueryParam rqp);
    }
}
