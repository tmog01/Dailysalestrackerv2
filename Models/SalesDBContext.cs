using Microsoft.EntityFrameworkCore;

namespace DailySalesTracker.Models
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

        public DbSet<SalesData> SalesRecords { get; set; }
    }
}