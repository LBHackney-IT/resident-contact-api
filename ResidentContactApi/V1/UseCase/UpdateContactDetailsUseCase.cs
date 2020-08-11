using System;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.UseCase.Interfaces;
using ResidentContactApi.V1.Gateways;

namespace ResidentContactApi.V1.UseCase
{
    public class UpdateContactDetailsUseCase : IUpdateContactDetailsUseCase
    {
        private IResidentGateway _residentGateway;
        public UpdateContactDetailsUseCase(IResidentGateway residentGateway)
        {
            _residentGateway = residentGateway;
        }
        public ResidentResponse Execute(ResidentContactParam rcp)
        {
            throw new NotImplementedException();
        }
    }
}
