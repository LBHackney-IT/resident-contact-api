using AutoFixture;
using ResidentContactApi.Tests.V1.Helper;
using ResidentContactApi.V1.Domain;
using ResidentContactApi.V1.Gateways;
using FluentAssertions;
using NUnit.Framework;
using System.ComponentModel.Design.Serialization;
using ResidentContactApi.V1.Boundary.Response.Residents;

namespace ResidentContactApi.Tests.V1.Gateways
{
    //TODO: Rename Tests to match gateway name
    //For instruction on how to run tests please see the wiki: https://github.com/LBHackney-IT/lbh-base-api/wiki/Running-the-test-suite.
    [TestFixture]
    public class ResidentGatewayTests : DatabaseTests
    {
        //private readonly Fixture _fixture = new Fixture();
        private ResidentGateway _classUnderTest;

        [SetUp]
        public void Setup()
        {
            _classUnderTest = new ResidentGateway(DatabaseContext);
        }




        //TODO: Add tests here for the get all method.
    }
}
