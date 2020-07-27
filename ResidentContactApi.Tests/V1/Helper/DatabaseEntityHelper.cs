using AutoFixture;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Infrastructure;

namespace ResidentContactApi.Tests.V1.Helper
{
    public static class DatabaseEntityHelper
    {
        public static DatabaseEntity CreateDatabaseEntity()
        {
            var entity = new Fixture().Create<ResidentDomain>();

            return CreateDatabaseEntityFrom(entity);
        }

        public static DatabaseEntity CreateDatabaseEntityFrom(ResidentDomain entity)
        {
            return new DatabaseEntity
            {
                Id = entity.Id
            };
        }
    }
}
