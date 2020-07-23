using ResidentContactApi.V1.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;

namespace ResidentContactApi.Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        private IDbContextTransaction _transaction;
        protected ResidentContactContext DatabaseContext { get; private set; }

        [SetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseNpgsql(ConnectionString.TestDatabase());
            DatabaseContext = new ResidentContactContext(builder.Options);

            DatabaseContext.Database.EnsureCreated();
            _transaction = DatabaseContext.Database.BeginTransaction();
        }

        [TearDown]
        public void RunAfterAnyTests()
        {
            _transaction.Rollback();
            _transaction.Dispose();
        }
    }
}
