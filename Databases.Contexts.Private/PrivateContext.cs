using Microsoft.EntityFrameworkCore;

namespace eveDirect.Databases.Contexts
{
    public class PrivateContext : DbContext
    {
        // Contacts
        public DbSet<EveOnlineContact> Eveonline_Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EveOnlineContact>().HasAnnotation();
        }
    }

    public class EveOnlineContact
    {

    }
}
