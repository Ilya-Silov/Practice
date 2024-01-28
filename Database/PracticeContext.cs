using Microsoft.EntityFrameworkCore;
using practice.Models;

namespace practice.Database
{
    public class PracticeContext : DbContext
    {

        public DbSet<ActivitiesInformationSecurity> ActivitiesInformationSecurity { get; set; }
        public DbSet<City> City { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Direction> Directions { get; set; }
        public DbSet<ActionJury> ActionJury { get; set; }
        public DbSet<Activity> Activites { get; set; }
        public DbSet<Ivent> Ivents { get; set; }
        public DbSet<User> Users{ get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=hnt8.ru;Port=5432;Database=AKVTCommonPractice;UserID=admin;Password=admin");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("ne-trogat");
        }
    }
}
