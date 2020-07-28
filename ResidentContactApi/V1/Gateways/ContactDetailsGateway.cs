using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Infrastructure;

namespace ResidentContactApi.V1.Gateways
{
    public class ContactDetailsGateway : IContactDetailsGateway
    {
        private readonly ResidentContactContext _residentContactContext;

        public ContactDetailsGateway(ResidentContactContext residentContactContext)
        {
            _residentContactContext = residentContactContext;
        }
        public List<ContactDetailsDomain> GetContactDetails()
        {
            var residents = _residentContactContext.ContactDetails
                .ToDomain();


            if (!residents.Any())
                return new List<ContactDetailsDomain>();

            return residents;

        }



    }
}
