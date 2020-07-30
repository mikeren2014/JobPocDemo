using JobPocDemo.Data.EntityTypeConfigurations;
using Microsoft.EntityFrameworkCore;

namespace JobPocDemo.Data
{
    public class JobDbContext : DbContext
    {
        #region properties

        public DbSet<JobItem> JobItems { get; set; } = default!;

        #endregion

        #region methods

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=Job.db");

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new JobItemEntityTypeConfiguration());

            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}
