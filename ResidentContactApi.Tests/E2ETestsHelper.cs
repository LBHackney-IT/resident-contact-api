using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Infrastructure;
using System.Collections.Generic;
using AutoFixture;

namespace ResidentContactApi.Tests
{
    public static class E2ETestsHelper
    {
        public static ResidentResponse AddPersonWithRelatedEntitiestoDb(ResidentContactContext context, int? id = null, string firstname = null, string lastname = null)
        {
            var fixture = new Fixture();
            var resident = TestHelper.CreateDatabasePersonEntity(firstname, lastname, id);
            var addedPerson = context.Residents.Add(resident);
            context.SaveChanges();

            var contactType = new ContactTypeLookup { Name = fixture.Create<string>() };
            var subContactType = new ContactSubTypeLookup { Name = fixture.Create<string>() };
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
                Contacts =
                    new List<ContactDetailsResponse>
                    {
                        new ContactDetailsResponse
                        {

                            Id = contact.Id,
                            ContactValue = contact.ContactValue,
                            AddedBy = contact.AddedBy,
                            IsActive = contact.IsActive,
                            IsDefault = contact.IsDefault,
                            DateLastModified = contact.DateLastModified,
                            ModifiedBy = contact.ModifiedBy,
                            DateAdded = contact.DateAdded,
                            ResidentId = contact.ResidentId,
                            Type = contactType.Name,
                            SubType = subContactType.Name

                        }
                    }


            };

        }
    }
}
