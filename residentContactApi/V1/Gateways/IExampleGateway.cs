using System.Collections.Generic;
using residentContactApi.V1.Domain;

namespace residentContactApi.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
