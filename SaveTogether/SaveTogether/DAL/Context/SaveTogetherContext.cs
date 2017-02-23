using SaveTogether.DAL.Entities;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SaveTogether.DAL.Context
{
    public class SaveTogetherContext: DbContext
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
        public DbSet<Country> Countries { get; set; }
        public DbSet<Donation> Donations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}