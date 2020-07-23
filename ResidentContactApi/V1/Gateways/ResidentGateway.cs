using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.V1.Gateways
{
    public class ResidentGateway : IResidentGateway
    {
        private readonly ResidentContactContext _residentContactContext;

        public ResidentGateway(ResidentContactContext residentContactContext)
        {
            _residentContactContext = residentContactContext;
        }
        public Task<List<ResidentResponse>> GetResidents(ResidentQueryParam rqp)
        {
            return null;
        }
    }
}
