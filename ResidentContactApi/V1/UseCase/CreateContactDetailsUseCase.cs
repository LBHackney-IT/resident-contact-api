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
    public class CreateContactDetailsUseCase : ICreateContactDetailsUseCase
    {
        private readonly IResidentGateway _residentGateway;
        public CreateContactDetailsUseCase(IResidentGateway residentGateway)
        {
            _residentGateway = residentGateway;
        }
        public ContactDetailsResponse Execute(ResidentContact contactRequest)
        {
            if (contactRequest.ResidentId == null && string.IsNullOrWhiteSpace(contactRequest.NccContactId))
            {
                throw new NoIdentifierException();
            }

            var contactDomain = new ContactDetailsDomain
            {
                ContactValue = contactRequest.Value,
                IsActive = contactRequest.Active,
                IsDefault = contactRequest.Default,
                TypeId = contactRequest.TypeId,
                SubtypeId = contactRequest.SubtypeId
            };
            var response = _residentGateway.InsertResidentContactDetails(contactRequest.ResidentId,
                contactRequest.NccContactId, contactDomain);
            if (response == null) throw new ResidentNotFoundException();
            return new ContactDetailsResponse { Id = response.Value };
        }
    }
}



