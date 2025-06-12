using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProjectName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.About)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(200);


            builder.HasMany(p => p.userProjects)
                .WithOne(up => up.Project)
                .HasForeignKey(up => up.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(p => p.projectImages)
                .WithOne(up => up.Project)
                .HasForeignKey(up => up.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
