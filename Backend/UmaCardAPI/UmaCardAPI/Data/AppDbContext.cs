using UmaCardAPI.Models;
using Microsoft.EntityFrameworkCore;
//using Npgsql.EntityFrameworkCore;

namespace UmaCardAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UmaCard> UmaCards { get; set; }
    }
}
