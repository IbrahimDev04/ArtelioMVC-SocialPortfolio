using Artelio.MVC.Entities;
using Artelio.MVC.Entities.Common;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Artelio.MVC.Contexts
{
    public class ArtelioContext : IdentityDbContext<AppUser>
    {
        public DbSet<Award> Awards { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<SocialMediaLink> SocialMediaLinks { get; set; }
        public DbSet<UserAbility> UserAbilities { get; set; }
        public DbSet<UserAward> UserAwards { get; set; }
        public DbSet<UserEducation> UserEducations { get; set; }
        public DbSet<UserLanguage> UserLanguages { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<UserSocialMedia> UserSocialMedias { get; set; }
        public DbSet<Work> Works { get; set; }
        public DbSet<WorkExperience> WorkExperiences { get; set; }
        public DbSet<ProjectImages> ProjectImages { get; set; }
        public DbSet<ViewerProject> viewerProjects   { get; set; }
        public DbSet<Notification> notifications { get; set; }



        public ArtelioContext(DbContextOptions options) : base(options)
        {
        }


        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            ((BaseEntity)entry.Entity).CreateTime = DateTime.Now;
                            break;
                        case EntityState.Modified:
                            ((BaseEntity)entry.Entity).UpdateTime = DateTime.Now;
                            break;
                    }
                }

            }
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ArtelioContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
