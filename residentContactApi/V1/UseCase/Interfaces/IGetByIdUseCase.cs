using residentContactApi.V1.Boundary.Response;

namespace residentContactApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        ResponseObject Execute(int id);
    }
}
