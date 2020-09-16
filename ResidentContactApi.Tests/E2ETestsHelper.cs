using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Infrastructure;
using System.Collections.Generic;
using AutoFixture;

namespace ResidentContactApi.Tests
{
    public static class E2ETestsHelper
    {
        public static ResidentResponse AddPersonWithRelatedEntitiesToDb(ResidentContactContext context, int? id = null,
            string firstname = null, string lastname = null, int? contactTypeLookupId = null, int? contactSubTypeLookupId = null)
        {
            var fixture = new Fixture();
            var resident = TestHelper.CreateDatabasePersonEntity(firstname, lastname, id);
            var addedPerson = context.Residents.Add(resident);
            context.SaveChanges();

            var contactType = new ContactTypeLookup { Name = fixture.Create<string>() };
            contactType.Id = contactTypeLookupId ?? contactType.Id;

            var subContactType = new ContactSubTypeLookup { Name = fixture.Create<string>() };
            subContactType.Id = contactSubTypeLookupId ?? subContactType.Id;

            context.ContactTypeLookups.Add(contactType);
            context.ContactSubTypeLookups.Add(subContactType);
            context.SaveChanges();

            var contact = TestHelper.CreateDatabaseContactEntity(addedPerson.Entity.Id, contactType.Id, subContactType.Id);
            context.ContactDetails.Add(contact);
            context.SaveChanges();

            return new ResidentResponse
            {
                Id = resident.Id,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                Gender = "F",
                DateOfBirth = resident.DateOfBirth,
                ContactInformation =
                    new List<ContactDetailsResponse>
                    {
                        new ContactDetailsResponse
                        {

                            Id = contact.Id,
                            Value = contact.ContactValue,
                            AddedBy = contact.AddedBy,
                            Active = contact.IsActive,
                            Default = contact.IsDefault,
                            DateLastModified = contact.DateLastModified,
                            ModifiedBy = contact.ModifiedBy,
                            DateAdded = contact.DateAdded,
                            Type = contactType.Name,
                            SubType = subContactType.Name
                        }
                    }
            };
        }

        public static string AddCrmContactIdForResidentId(ResidentContactContext context, int residentId)
        {
            var fixture = new Fixture();
            var externalSystemLookup = new ExternalSystemLookup
            {
                Name = "CRM"
            };
            context.ExternalSystemLookups.Add(externalSystemLookup);
            context.SaveChanges();
            var externalLink = new ExternalSystemId
            {
                ResidentId = residentId,
                ExternalIdName = "ContactId",
                ExternalSystemLookupId = externalSystemLookup.Id,
                ExternalIdValue = fixture.Create<string>()
            };
            context.ExternalSystemIds.Add(externalLink);
            context.SaveChanges();
            return externalLink.ExternalIdValue;
        }
    }
}
