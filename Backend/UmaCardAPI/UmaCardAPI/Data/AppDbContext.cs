using UmaCardAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace UmaCardAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UmaCard> UmaCards { get; set; }
    }
}
