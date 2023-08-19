using Microsoft.EntityFrameworkCore;
using prjSchaumburg.Models.Domain;

namespace prjSchaumburg.Data
{
    public class MVCDbContext : DbContext
    {
        public MVCDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<Machine> Machines { get; set; }
        public DbSet<Component> Components { get; set; }
    }
}
