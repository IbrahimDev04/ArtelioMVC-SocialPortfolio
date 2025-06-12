using Artelio.MVC.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Artelio.MVC.Configurations
{
    public class UserAbilityConfiguration : IEntityTypeConfiguration<UserAbility>
    {
        public void Configure(EntityTypeBuilder<UserAbility> builder)
        {
            builder.HasKey(ua => ua.Id);

            builder.Property(ua => ua.AbilityName)
                .IsRequired()
                .HasMaxLength(150);

            builder.HasOne(ua => ua.User)
                .WithMany(u => u.userAbilities)
                .HasForeignKey(ua => ua.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
