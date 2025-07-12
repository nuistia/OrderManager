using Hometask.OrderManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hometask.OrderManager.Data.EF;

public partial class CHIITDbContext : DbContext
{
    public virtual DbSet<Analysis> Analyses { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Order> Orders { get; set; }
    
    public CHIITDbContext(DbContextOptions<CHIITDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CHIITDbContext).Assembly);
    }
}
