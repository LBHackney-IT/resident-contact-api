using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Infrastructure;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ResidentContactApi.V1.Factories
{
    public static class EntityFactory
    {
        public static ResidentDomain ToDomain(this ResidentsInfra databaseEntity)
        {
            return new ResidentDomain
            {

                FirstName = databaseEntity.FirstName.Trim(),
                LastName = databaseEntity.LastName.Trim(),
                Gender = databaseEntity.Gender
            };
        }

        public static List<ResidentDomain> ToDomain(this IEnumerable<ResidentsInfra> people)
        {
            return people.Select(p => p.ToDomain()).ToList();
        }
    }
}
