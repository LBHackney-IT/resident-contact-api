using Microsoft.EntityFrameworkCore;

namespace ResidentContactApi.V1.Infrastructure
{

    public class ResidentContactContext : DbContext
    {
        public ResidentContactContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ResidentsInfra> Residents { get; set; }
        public DbSet<ContactDetailsInfra> ContactDetails { get; set; }


    }
}
