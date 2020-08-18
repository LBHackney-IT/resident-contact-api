using ResidentContactApi.V1.Boundary.Requests;
using ResidentContactApi.V1.Boundary.Response;

namespace ResidentContactApi.V1.UseCase.Interfaces
{
    public interface ICreateContactDetailsUseCase
    {
        ResidentResponse Execute(ResidentContact rcp);
    }
}



