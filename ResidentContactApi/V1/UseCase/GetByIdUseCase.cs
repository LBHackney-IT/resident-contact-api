using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;

namespace ResidentContactApi.V1.UseCase
{
    public class GetByIdUseCase : IGetByIdUseCase
    {
        private IContactDetailsGateway _gateway;
        public GetByIdUseCase(IContactDetailsGateway gateway)
        {
            _gateway = gateway;
        }
        public ContactDetailsResponse Execute(int id)
        {
            return null;
        }
    }
}
