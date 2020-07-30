using Microsoft.EntityFrameworkCore;

namespace ResidentContactApi.V1.Infrastructure
{

    public class ResidentContactContext : DbContext
    {
        public ResidentContactContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Resident> Residents { get; set; }
        public DbSet<Contact> ContactDetails { get; set; }



    }
}
