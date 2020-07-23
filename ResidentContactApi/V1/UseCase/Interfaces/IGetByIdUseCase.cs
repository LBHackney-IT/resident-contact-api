using ResidentContactApi.V1.Boundary.Response;

namespace ResidentContactApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        ContactDetailsResponse Execute(int id);
    }
}
