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
        public DbSet<Moderator> Moderators { get; set; }
        public DbSet<Organizers> Organizers { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Jury> Jury { get; set; }
        public DbSet<ActionJury> ActionJury { get; set; }
        public DbSet<Action> Action { get; set; }
        public DbSet<Ivent> Ivent { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=hnt8.ru;Port=5432;Database=AKVTCommonPractice;UserID=admin;Password=admin");
        }
    }
}
