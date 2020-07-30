using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Enums;
using ResidentContactApi.V1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ResidentContactApi.V1.Factories
{
    public static class EntityFactory
    {

        public static ResidentDomain ToDomain(this Resident databaseEntity)
        {
            return new ResidentDomain
            {
                FirstName = databaseEntity.FirstName.Trim(),
                LastName = databaseEntity.LastName.Trim(),
                Gender = databaseEntity.Gender,
                Contacts = databaseEntity.Contacts?.ToDomain()
            };
        }

        public static List<ResidentDomain> ToDomain(this IEnumerable<Resident> people)
        {
            return people.Select(p => p.ToDomain()).ToList();
        }

        public static ContactDetailsDomain ToDomain(this Contact contactDetails)
        {
            var canParseType = Enum.TryParse<ContactTypeEnum>(contactDetails.ContactType, out var type);
            var canParseSubType = Enum.TryParse<ContactSubTypeEnum>(contactDetails.SubContactType, out var subtype);
            return new ContactDetailsDomain
            {
                Type = canParseType ? type : ContactTypeEnum.NotApplicable,
                SubType = canParseSubType ? subtype : ContactSubTypeEnum.NotApplicable,
                Id = contactDetails.Id,
                ContactValue = contactDetails.ContactValue,
                AddedBy = contactDetails.AddedBy,
                IsActive = contactDetails.IsActive,
                IsDefault = contactDetails.IsDefault,
                DateLastModified = contactDetails.DateLastModified,
                ModifiedBy = contactDetails.ModifiedBy,
                DateAdded = contactDetails.DateAdded,
                ResidentId = contactDetails.ResidentId


            };

        }

        public static List<ContactDetailsDomain> ToDomain(this IEnumerable<Contact> people)
        {
            return people.Select(p => p.ToDomain()).ToList();
        }
    }
}
