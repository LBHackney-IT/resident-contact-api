using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;

namespace ResidentContactApi.V1.UseCase
{
    public class GetByIdUseCase : IGetByIdUseCase
    {
        private IResidentGateway _gateway;
        public GetByIdUseCase(IResidentGateway gateway)
        {
            _gateway = gateway;
        }
        public ResidentResponse Execute(int id)
        {
            var residentInfo = _gateway.GetResidentById(id);

            if (residentInfo == null) throw new ResidentNotFoundException();
            return residentInfo.ToResponse();
        }
    }
}
