using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Response.ContactDetails;
using ResidentContactApi.V1.Boundary.Response.Residents;

namespace ResidentContactApi.V1.UseCase.Interfaces
{
    public interface IGetAllUseCase
    {
        ResidentResponseList Execute(ResidentQueryParam rqp);
    }
}
