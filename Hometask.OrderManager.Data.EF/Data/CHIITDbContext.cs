using Hometask.OrderManager.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Hometask.OrderManager.Data.Data;

public partial class CHIITDbContext : DbContext
{
    public CHIITDbContext()
    {
    }

    public CHIITDbContext(DbContextOptions<CHIITDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Analysis> Analyses { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=CHI_IT_TASK;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Analysis>(entity =>
        {
            entity.HasKey(e => e.AnId).HasName("PK__Analysis__831DABF3BC7A571F");

            entity.ToTable("Analysis");

            entity.Property(e => e.AnId)
                .ValueGeneratedNever()
                .HasColumnName("an_id");
            entity.Property(e => e.AnCost)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("an_cost");
            entity.Property(e => e.AnGroup).HasColumnName("an_group");
            entity.Property(e => e.AnName)
                .HasMaxLength(100)
                .HasColumnName("an_name");
            entity.Property(e => e.AnPrice)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("an_price");

            entity.HasOne(d => d.AnGroupNavigation).WithMany(p => p.Analyses)
                .HasForeignKey(d => d.AnGroup)
                .HasConstraintName("FK_Analysis_Groups");
        });

        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.GrId).HasName("PK__Groups__2BC0F88E02FCCB78");

            entity.Property(e => e.GrId)
                .ValueGeneratedNever()
                .HasColumnName("gr_id");
            entity.Property(e => e.GrName)
                .HasMaxLength(50)
                .HasColumnName("gr_name");
            entity.Property(e => e.GrTemp)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("gr_temp");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrdId).HasName("PK__Orders__DC39D7DF5F23AF78");

            entity.Property(e => e.OrdId)
                .ValueGeneratedNever()
                .HasColumnName("ord_id");
            entity.Property(e => e.OrdAn).HasColumnName("ord_an");
            entity.Property(e => e.OrdDatetime)
                .HasColumnType("datetime")
                .HasColumnName("ord_datetime");

            entity.HasOne(d => d.OrdAnNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.OrdAn)
                .HasConstraintName("FK_Orders_Analysis");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
