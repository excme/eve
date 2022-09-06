using eveDirect.Shared.ConfigurationHelper;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace eveDirect.Translation.DbContext
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public ApplicationDbContext() { }

        /// <summary>
        /// Переводы
        /// </summary>
        public DbSet<Translation> Translations { get; set; }
        /// <summary>
        /// Версии переводов
        /// </summary>
        public DbSet<TranslationVersion> TranslationVersions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Translation>().Property(m => m.ru).HasColumnType("jsonb");
            modelBuilder.Entity<Translation>().Property(m => m.en).HasColumnType("jsonb");
            modelBuilder.Entity<Translation>().Property(m => m.ge).HasColumnType("jsonb");
            modelBuilder.Entity<Translation>().Property(m => m.fr).HasColumnType("jsonb");
            modelBuilder.Entity<Translation>().Property(m => m.ja).HasColumnType("jsonb");
            modelBuilder.Entity<Translation>().Property(m => m.ko).HasColumnType("jsonb");
            modelBuilder.Entity<Translation>().Property(m => m.zh).HasColumnType("jsonb");
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var connStr = ConfigurationStatic.LoadConnectionString("TranslateDb");
                optionsBuilder = ContextStatic.DbContextOptions(connStr);
            }

            base.OnConfiguring(optionsBuilder);
        }
    }
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            var connStr = ConfigurationStatic.LoadConnectionString("TranDb");
            optionsBuilder.UseNpgsql(connStr);

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}
