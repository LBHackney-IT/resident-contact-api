using Microsoft.EntityFrameworkCore;

namespace ResidentContactApi.V1.Infrastructure
{

    public class ResidentContactContext : DbContext
    {
        //TODO: rename DatabaseContext to reflect the data source it is representing. eg. MosaicContext.
        //Guidance on the context class can be found here https://github.com/LBHackney-IT/lbh-base-api/wiki/DatabaseContext
        public ResidentContactContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<DatabaseEntity> DatabaseEntities { get; set; }
    }
}
