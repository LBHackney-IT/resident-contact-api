using AutoFixture;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Infrastructure;
using System;
using System.Net.NetworkInformation;

namespace ResidentContactApi.Tests.V1.Helper
{
    public static class TestHelper
    {
        public static Resident CreateDatabasePersonEntity(string firstname = null, string lastname = null)
        {
            var faker = new Fixture();
            var fp = faker.Build<Resident>()
                .Without(contact => contact.Contacts)
                .Create();
            fp.DateOfBirth = new DateTime
                (fp.DateOfBirth.Year, fp.DateOfBirth.Month, fp.DateOfBirth.Day);
            fp.FirstName = firstname ?? fp.FirstName;
            fp.LastName = lastname ?? fp.LastName;
            return fp;
        }

        public static Contact CreateDatabaseContactEntity(int residentId)
        {
            var faker = new Fixture();
            var fp = faker.Build<Contact>()
                .Without(resident => resident.Resident)
                .Create();
            fp.ResidentId = residentId;
            return fp;
        }
    }
}
