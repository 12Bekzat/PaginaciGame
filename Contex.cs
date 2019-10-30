using HW7.Models;
using Microsoft.EntityFrameworkCore;

namespace GamePlayPagination
{
    public class Contex : DbContext
    {
        private readonly string connectionString;

        public Contex(string connectionString)
        {
            this.connectionString = connectionString;
            Database.EnsureCreated();
        }

        public DbSet<Game> VideoGames { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        // и точно такие же другие сущности

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
