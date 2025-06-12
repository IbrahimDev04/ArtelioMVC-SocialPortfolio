using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class SocialMediaLinkConfiguration : IEntityTypeConfiguration<SocialMediaLink>
    {
        public void Configure(EntityTypeBuilder<SocialMediaLink> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(s => s.TwitterUrl).HasMaxLength(250);
            builder.Property(s => s.LinkedInUrl).HasMaxLength(250);
            builder.Property(s => s.InstagramUrl).HasMaxLength(250);
            builder.Property(s => s.FacebookUrl).HasMaxLength(250);
            builder.Property(s => s.YouTubeUrl).HasMaxLength(250);
            builder.Property(s => s.GitHubUrl).HasMaxLength(250);

            builder.HasMany(s => s.userSocialMedias)
                .WithOne(usm => usm.SocialMedia)
                .HasForeignKey(usm => usm.SocialMediaId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
