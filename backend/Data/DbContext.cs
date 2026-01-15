using Microsoft.EntityFrameworkCore;
using Leads.Model;

namespace MinhaApiRest.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Lead> Leads { get; set; }
    }
}
