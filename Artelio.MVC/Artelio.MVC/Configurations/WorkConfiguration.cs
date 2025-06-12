using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class WorkConfiguration : IEntityTypeConfiguration<Work>
    {
        public void Configure(EntityTypeBuilder<Work> builder)
        {
            builder.HasKey(w => w.Id);

            builder.Property(w => w.Company)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(w => w.JobName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(w => w.StatDate)
                .IsRequired();

            builder.Property(w => w.EndDate);

            builder.Property(w => w.City)
                .HasMaxLength(100);

            builder.Property(w => w.PhoneNumber)
                .HasMaxLength(20);

            builder.Property(w => w.About)
                .HasMaxLength(1000);

            builder.HasMany(w => w.workExperiences)
                .WithOne(we => we.Work)
                .HasForeignKey(we => we.WorkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
