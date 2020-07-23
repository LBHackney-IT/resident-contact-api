using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;

namespace ResidentContactApi.V1.UseCase
{
    //TODO: Rename class name and interface name to reflect the entity they are representing eg. GetClaimantByIdUseCase
    public class GetByIdUseCase : IGetByIdUseCase
    {
        private IContactDetailsGateway _gateway;
        public GetByIdUseCase(IContactDetailsGateway gateway)
        {
            _gateway = gateway;
        }

        //TODO: rename id to the name of the identifier that will be used for this API, the type may also need to change
        public ContactDetailsResponse Execute(int id)
        {
            return null;
        }
    }
}
