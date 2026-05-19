using Microsoft.EntityFrameworkCore;
using RawPace.Models;


namespace RawPace.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Bowler> Bowlers { get; set; }
        public DbSet<User> Users { get; set; }



    }
}
