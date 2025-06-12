using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class UserAwardConfiguration : IEntityTypeConfiguration<UserAward>
    {
        public void Configure(EntityTypeBuilder<UserAward> builder)
        {
            builder.HasKey(ua => ua.Id);

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.userAwards)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ua => ua.Award)
                .WithMany()
                .HasForeignKey(ua => ua.AwardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
