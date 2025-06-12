using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class UserSocialMediaConfiguration : IEntityTypeConfiguration<UserSocialMedia>
    {
        public void Configure(EntityTypeBuilder<UserSocialMedia> builder)
        {
            builder.HasKey(usm => usm.Id);

            builder.HasOne(usm => usm.User)
                .WithMany(u => u.userSocialMedias)
                .HasForeignKey(usm => usm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(usm => usm.SocialMedia)
                .WithMany(s => s.userSocialMedias)
                .HasForeignKey(usm => usm.SocialMediaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
