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
            var canParseGender = Enum.TryParse<GenderTypeEnum>(databaseEntity.Gender?.ToString(), out var gender);

            return new ResidentDomain
            {
                Id = databaseEntity.Id,
                FirstName = databaseEntity.FirstName?.Trim(),
                LastName = databaseEntity.LastName?.Trim(),
                Gender = canParseGender ? gender : GenderTypeEnum.Unknown,
                DateOfBirth = databaseEntity.DateOfBirth,
                Contacts = databaseEntity.Contacts?.ToDomain()
            };
        }

        public static List<ResidentDomain> ToDomain(this IEnumerable<Resident> people)
        {
            return people.Select(p => p.ToDomain()).ToList();
        }

        public static ContactDetailsDomain ToDomain(this Contact contactDetails)
        {
            return new ContactDetailsDomain
            {
                Type = contactDetails.ContactTypeLookup?.Name,
                SubType = contactDetails.ContactSubTypeLookup?.Name,
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

        public static List<ContactDetailsDomain> ToDomain(this IEnumerable<Contact> contacts)
        {
            return contacts.Select(p => p.ToDomain()).ToList();
        }
    }
}
