using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response.Residents;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public List<ResidentDomain> GetResidents(string firstName = null, string lastName = null)
        {
            var residents = _residentContactContext.Residents
                .Where(a => string.IsNullOrEmpty(firstName) || a.FirstName.Trim().ToLower().Contains(firstName.ToLower()))
                .Where(a => string.IsNullOrEmpty(lastName) || a.LastName.Trim().ToLower().Contains(lastName.ToLower()))
                .ToDomain();

            if (!residents.Any())
                return new List<ResidentDomain>();

            return residents;

        }


    }
}
