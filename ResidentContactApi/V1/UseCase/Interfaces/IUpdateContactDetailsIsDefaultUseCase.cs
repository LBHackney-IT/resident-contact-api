using ResidentContactApi.V1.Boundary.Requests;

namespace ResidentContactApi.V1.UseCase.Interfaces
{
    public interface IUpdateContactDetailsIsDefaultUseCase
    {
        bool Execute(int id, ContactDetails request);
    }
}
