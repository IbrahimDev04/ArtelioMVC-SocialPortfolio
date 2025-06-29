using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Surname)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.ImageUrl).HasMaxLength(250);
            builder.Property(u => u.BannerUrl).HasMaxLength(250);
            builder.Property(u => u.About).HasMaxLength(1000);
            builder.Property(u => u.Job).HasMaxLength(100);
            builder.Property(u => u.Company).HasMaxLength(100);
            builder.Property(u => u.Country).HasMaxLength(100);
            builder.Property(u => u.City).HasMaxLength(100);
            builder.Property(u => u.WebUrl).HasMaxLength(250);

            // Relations

            builder.HasMany(u => u.userSocialMedias)
                .WithOne(usm => usm.User)
                .HasForeignKey(usm => usm.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.workExperiences)
                .WithOne(we => we.User)
                .HasForeignKey(we => we.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.userEducations)
                .WithOne(ue => ue.User)
                .HasForeignKey(ue => ue.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.userAwards)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.userAbilities)
                .WithOne(ua => ua.User)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.userLanguages)
                .WithOne(ul => ul.User)
                .HasForeignKey(ul => ul.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.userProjects)
                .WithOne(up => up.User)
                .HasForeignKey(up => up.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(u => u.followings)
                .WithOne(f => f.Following)
                .HasForeignKey(f => f.FollowingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.followeds)
                .WithOne(f => f.Followed)
                .HasForeignKey(f => f.FollowedId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.viewerProjects)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(u => u.notifications)
                .WithOne(f => f.User)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.from)
                .WithOne(f => f.From)
                .HasForeignKey(f => f.FromId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(u => u.to)
                .WithOne(f => f.To)
                .HasForeignKey(f => f.ToId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
