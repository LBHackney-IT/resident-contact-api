using Microsoft.AspNetCore.Mvc;
using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response.Residents;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;

namespace ResidentContactApi.V1.UseCase
{
    public class GetAllUseCase : IGetAllUseCase
    {
        private IResidentGateway _residentGateway;
        public GetAllUseCase(IResidentGateway gateway)
        {
            _residentGateway = gateway;
        }

        public ResidentResponseList Execute(ResidentQueryParam rqp)
        {
            var residents = _residentGateway.GetResidents(rqp.FirstName, rqp.LastName).ToResponse();

            return new ResidentResponseList
            {
                Residents = residents
            };
        }

    }
}


