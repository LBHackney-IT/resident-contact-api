using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Infrastructure;

namespace ResidentContactApi.V1.Factories
{
    public static class EntityFactory
    {
        public static ContactDetailsResponse ToDomain(this DatabaseEntity databaseEntity)
        {
            //TODO: Map the rest of the fields in the domain object.
            // More information on this can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/Factory-object-mappings

            return new ContactDetailsResponse
            {
                Id = databaseEntity.Id

            };
        }
    }
}
