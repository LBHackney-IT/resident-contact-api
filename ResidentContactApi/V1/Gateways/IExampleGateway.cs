using System.Collections.Generic;
using ResidentContactApi.V1.Domain;

namespace ResidentContactApi.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
