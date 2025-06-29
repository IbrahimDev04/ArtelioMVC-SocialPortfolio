using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class EducationConfiguration : IEntityTypeConfiguration<Education>
    {
        public void Configure(EntityTypeBuilder<Education> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.SchoolName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(e => e.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Degree)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.StartDate)
                .IsRequired();

            builder.Property(e => e.EndDate);

            builder.Property(e => e.About)
                .HasMaxLength(5000);

            builder.HasMany(e => e.userEducations)
                .WithOne(ue => ue.Education)
                .HasForeignKey(ue => ue.EducationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
