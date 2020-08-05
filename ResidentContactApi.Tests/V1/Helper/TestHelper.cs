using AutoFixture;
using ResidentContactApi.V1.Infrastructure;
using System;

namespace ResidentContactApi.Tests.V1.Helper
{
    public static class TestHelper
    {
        public static Resident CreateDatabasePersonEntity(string firstname = null, string lastname = null, int? id = null)
        {
            var faker = new Fixture();
            var fp = faker.Build<Resident>()
                .Without(contact => contact.Contacts)
                .With(contact => contact.Gender, 'F')
                .Create();
            fp.DateOfBirth = new DateTime
                (fp.DateOfBirth.Value.Year, fp.DateOfBirth.Value.Month, fp.DateOfBirth.Value.Day);
            fp.FirstName = firstname ?? fp.FirstName;
            fp.LastName = lastname ?? fp.LastName;
            if (id != null) fp.Id = (int) id;
            return fp;
        }

        public static Contact CreateDatabaseContactEntity(int residentId, int? contactTypeId = null, int? contactSubtypeId = null)
        {
            var faker = new Fixture();
            var fp = faker.Build<Contact>()
                .Without(contact => contact.Resident)
                .Without(contact => contact.ContactSubTypeLookup)
                .Without(contact => contact.ContactTypeLookup)
                .Create();
            fp.DateAdded = new DateTime
                (fp.DateAdded.Year, fp.DateAdded.Month, fp.DateAdded.Day);
            fp.DateLastModified = new DateTime
                (fp.DateLastModified.Year, fp.DateLastModified.Month, fp.DateLastModified.Day);
            fp.ResidentId = residentId;
            fp.ContactTypeLookupId = contactTypeId ?? 0;
            fp.ContactSubTypeLookupId = contactSubtypeId;
            return fp;
        }
    }
}
