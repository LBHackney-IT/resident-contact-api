using FluentAssertions;
using NUnit.Framework;
using ResidentContactApi.Tests.V1.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResidentContactApi.Tests.V1.Infrastructure
{
    [TestFixture]

    public class ResidentContactContextTest : DatabaseTests
    {
        public void CanGetADatabaseEntity()
        {
            var databaseEntity = TestHelper.CreateDatabasePersonEntity();

            ResidentContactContext.Add(databaseEntity);
            ResidentContactContext.SaveChanges();

            var result = ResidentContactContext.Residents.ToList().FirstOrDefault();

            result.Should().BeEquivalentTo(databaseEntity);
        }


    }
}
