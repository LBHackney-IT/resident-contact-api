using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Response.Residents;

namespace ResidentContactApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        ResidentResponse Execute(int id);
    }
}
