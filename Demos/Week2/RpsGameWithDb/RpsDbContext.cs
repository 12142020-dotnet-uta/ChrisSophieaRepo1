using Microsoft.EntityFrameworkCore;

namespace RpsGameWithDb
{
    public class RpsDbContext : DbContext
    {
        public DbSet<Player> players { get; set; }
        public DbSet<Round> rounds { get; set; }
        public DbSet<Match> matches { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=RPSGame;Trusted_Connection=True;");
        }

    }
}