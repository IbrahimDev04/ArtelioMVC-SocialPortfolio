using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations;

public class AwardConfiguration : IEntityTypeConfiguration<Award>
{
    public void Configure(EntityTypeBuilder<Award> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Company)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(a => a.AwardName)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(a => a.Date)
            .IsRequired()
            .HasMaxLength(50);
    }
}

