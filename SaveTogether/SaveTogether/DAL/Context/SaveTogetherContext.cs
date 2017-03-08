using SaveTogether.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SaveTogether.DAL.Context
{
    public class SaveTogetherContext: IdentityDbContext<AuthorizedPerson>
    {
        public SaveTogetherContext(): base("SaveTogetherContext") { }

        public static SaveTogetherContext Create()
        {
            return new SaveTogetherContext();
        }

        public DbSet<Administrator> Administrators { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Subscriber> Subscribers { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}