using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace JobPocDemo.Data.EntityTypeConfigurations
{
    public class JobItemEntityTypeConfiguration : IEntityTypeConfiguration<JobItem>
    {
        #region methods

        public void Configure(EntityTypeBuilder<JobItem> builder)
        {
            builder.ToTable("Jobs")
                   .HasKey(_ => _.Id);

            builder.Property(_ => _.Content)
                   .IsRequired();

            builder.Property(_ => _.Type)
                   .IsRequired();
        }

        #endregion
    }
}
