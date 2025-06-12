using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class UserEducationConfiguration : IEntityTypeConfiguration<UserEducation>
    {
        public void Configure(EntityTypeBuilder<UserEducation> builder)
        {
            builder.HasKey(ue => ue.Id);

            builder.HasOne(ue => ue.User)
                .WithMany(u => u.userEducations)
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ue => ue.Education)
                .WithMany(e => e.userEducations)
                .HasForeignKey(ue => ue.EducationId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
