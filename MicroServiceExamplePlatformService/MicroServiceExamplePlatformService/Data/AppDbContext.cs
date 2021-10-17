using MicroServiceExamplePlatformService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroServiceExamplePlatformService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Platform> Platforms { get; set; }
    }
}