using AutoFixture;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Infrastructure;
using System;

namespace ResidentContactApi.Tests.V1.Helper
{
    public static class TestHelper
    {
        public static ResidentsInfra CreateDatabasePersonEntity(string firstname = null, string lastname = null)
        {
            var faker = new Fixture();
            var fp = faker.Build<ResidentsInfra>()
                .Create();
            fp.DateOfBirth = new DateTime
                (fp.DateOfBirth.Year, fp.DateOfBirth.Month, fp.DateOfBirth.Day);
            fp.FirstName = firstname ?? fp.FirstName;
            fp.LastName = lastname ?? fp.LastName;
            return fp;
        }
    }
}
