using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;
using System;

namespace ResidentContactApi.V1.UseCase
{
    public class CreateContactDetailsUseCase : ICreateContactDetailsUseCase
    {
        private IResidentGateway _residentGateway;
        public CreateContactDetailsUseCase(IResidentGateway residentGateway)
        {
            _residentGateway = residentGateway;
        }
        public ResidentResponse Execute(ResidentContact rcp)
        {
            return _residentGateway.InsertResidentContactDetails(rcp).ToResponse();
        }
    }
}



