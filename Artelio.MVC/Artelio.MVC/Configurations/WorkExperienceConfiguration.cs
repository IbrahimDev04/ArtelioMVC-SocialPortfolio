using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class WorkExperienceConfiguration : IEntityTypeConfiguration<WorkExperience>
    {
        public void Configure(EntityTypeBuilder<WorkExperience> builder)
        {
            builder.HasKey(we => we.Id);

            builder.HasOne(we => we.User)
                .WithMany(u => u.workExperiences)
                .HasForeignKey(we => we.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(we => we.Work)
                .WithMany(w => w.workExperiences)
                .HasForeignKey(we => we.WorkId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
