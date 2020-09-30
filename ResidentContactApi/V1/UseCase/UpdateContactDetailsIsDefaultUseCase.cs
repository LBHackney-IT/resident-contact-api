using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;
using System;
using ResidentContactApi.V1.Boundary;
using ResidentContactApi.V1.Domain;

namespace ResidentContactApi.V1.UseCase
{
    public class UpdateContactDetailsIsDefaultUseCase : IUpdateContactDetailsIsDefaultUseCase
    {
        private readonly IContactDetailsGateway _contactDetailsGateway;
        private readonly IResidentGateway _residentGateway;
        public UpdateContactDetailsIsDefaultUseCase(IContactDetailsGateway contactDetailsGateway, IResidentGateway residentGateway)
        {
            _contactDetailsGateway = contactDetailsGateway;
            _residentGateway = residentGateway;
        }
        public bool Execute(int id, ContactDetails request)
        {

            var value = request.IsDefault;
            
            var contact = _contactDetailsGateway.GetContactById(id);

            if (contact == null) throw new ContactNotFoundException();
            //If we're changing a contact to true, we have to check we unset any existing true contacts
            if (value == true)
            {
                var residentContacts = _residentGateway.GetResidentById(contact.ResidentId).Contacts;

                foreach (var item in residentContacts)
                {
                    if (item.TypeId == contact.TypeId)
                    {
                        if (_contactDetailsGateway.UpdateContactIsDefault(item.Id, false) == false) throw new ContactNotFoundException();
                    }
                }
            }

            if (_contactDetailsGateway.UpdateContactIsDefault(id, value) == false) throw new ContactNotFoundException();

            return true;
        }
    }
}
