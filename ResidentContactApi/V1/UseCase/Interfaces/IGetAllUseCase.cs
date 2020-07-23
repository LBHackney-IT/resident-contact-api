using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Response.ContactDetails;

namespace ResidentContactApi.V1.UseCase.Interfaces
{
    public interface IGetAllUseCase
    {
        ContactDetailsResponseList Execute(ResidentQueryParam rqp);
    }
}
