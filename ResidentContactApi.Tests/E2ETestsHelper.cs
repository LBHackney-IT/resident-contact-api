using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Boundary.Response;
using ResidentContactApi.V1.Boundary.Response.Residents;
using ResidentContactApi.V1.Enums;
using ResidentContactApi.V1.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.Tests
{
    public static class E2ETestsHelper
    {
        public static ResidentResponse AddPersonWithRelatedEntitiestoDb(ResidentContactContext context, int? id = null, string firstname = null, string lastname = null)
        {
            var resident = TestHelper.CreateDatabasePersonEntity(firstname, lastname, id);
            var addedPerson = context.Residents.Add(resident);
            context.SaveChanges();

            var contact = TestHelper.CreateDatabaseContactEntity(addedPerson.Entity.Id);
            contact.ContactType = "Address";
            contact.SubContactType = "Mobile";
            context.ContactDetails.Add(contact);
            context.SaveChanges();

            return new ResidentResponse
            {
                Id = resident.Id,
                FirstName = resident.FirstName,
                LastName = resident.LastName,
                Gender = resident.Gender,
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
                            Type = ContactTypeEnum.Address,
                            SubType = ContactSubTypeEnum.Mobile

                        }
                    }


            };

        }
    }
}
