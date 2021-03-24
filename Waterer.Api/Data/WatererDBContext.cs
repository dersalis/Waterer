using Microsoft.EntityFrameworkCore;
using Waterer.Api.Models;

namespace Waterer.Api.Data
{
    public class WatererDBContext : DbContext
    {
        public WatererDBContext(DbContextOptions<WatererDBContext> options) : base(options){}

        public DbSet<User> Users { get; set; }
        public DbSet<Plant> Plants { get; set; }
        public DbSet<PlantState> plantStates { get; set; }
        public DbSet<PlantImage> plantImages { get; set; }
    }
}