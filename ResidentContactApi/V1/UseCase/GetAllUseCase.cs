using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Response.ContactDetails;
using ResidentContactApi.V1.Factories;
using ResidentContactApi.V1.Gateways;
using ResidentContactApi.V1.UseCase.Interfaces;

namespace ResidentContactApi.V1.UseCase
{
    //TODO: Rename class name and interface name to reflect the entity they are representing eg. GetAllClaimantsUseCase
    public class GetAllUseCase : IGetAllUseCase
    {
        private readonly IContactDetailsGateway _gateway;
        public GetAllUseCase(IContactDetailsGateway gateway)
        {
            _gateway = gateway;
        }

        public ContactDetailsResponseList Execute(ResidentQueryParam rqp)
        {
            return new ContactDetailsResponseList();
        }
    }
}
