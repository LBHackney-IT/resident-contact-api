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

        public static ContactDetailsDomain ToDomain(this ContactDetailsInfra contactDetails)
        {
            return new ContactDetailsDomain
            {
                Id = contactDetails.Id,
                ContactType = contactDetails.ContactType,
                ContactValue = contactDetails.ContactValue,
                AddedBy = contactDetails.AddedBy,
                IsActive = contactDetails.IsActive,
                IsDefault = contactDetails.IsDefault,
                DateLastModified = contactDetails.DateLastModified,
                ModifiedBy = contactDetails.ModifiedBy,
                DateAdded = contactDetails.DateAdded
            };
        }

        public static List<ContactDetailsDomain> ToDomain(this IEnumerable<ContactDetailsInfra> people)
        {
            return people.Select(p => p.ToDomain()).ToList();
        }
    }
}
