using Microsoft.EntityFrameworkCore;
using CatControl.API.Models;

namespace CatControl.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    // DbSets
    public DbSet<User> Users { get; set; }
    public DbSet<Cat> Cats { get; set; }
    public DbSet<Vaccine> Vaccines { get; set; }
    public DbSet<Deworming> Dewormings { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Hygiene> Hygienes { get; set; }
    public DbSet<Finance> Finances { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Wishlist> Wishlists { get; set; }
    public DbSet<WeightHistory> WeightHistories { get; set; }
    public DbSet<VeterinaryVisit> VeterinaryVisits { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // User relationships
        modelBuilder.Entity<User>()
            .HasMany(u => u.Cats)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Cat relationships
        modelBuilder.Entity<Cat>()
            .HasMany(c => c.Vacinas)
            .WithOne(v => v.Cat)
            .HasForeignKey(v => v.CatId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Cat>()
            .HasMany(c => c.Vermifugos)
            .WithOne(d => d.Cat)
            .HasForeignKey(d => d.CatId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Stock relationship
        modelBuilder.Entity<Stock>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Hygiene relationship
        modelBuilder.Entity<Hygiene>()
            .HasOne(h => h.Cat)
            .WithMany()
            .HasForeignKey(h => h.CatId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Finance relationship
        modelBuilder.Entity<Finance>()
            .HasOne(f => f.User)
            .WithMany()
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Finance>()
            .HasOne(f => f.Cat)
            .WithMany()
            .HasForeignKey(f => f.CatId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Budget relationship
        modelBuilder.Entity<Budget>()
            .HasOne(b => b.User)
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Wishlist relationship
        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.User)
            .WithMany()
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Wishlist>()
            .HasOne(w => w.Cat)
            .WithMany()
            .HasForeignKey(w => w.CatId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // WeightHistory relationship
        modelBuilder.Entity<WeightHistory>()
            .HasOne(w => w.Cat)
            .WithMany()
            .HasForeignKey(w => w.CatId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // VeterinaryVisit relationship
        modelBuilder.Entity<VeterinaryVisit>()
            .HasOne(v => v.Cat)
            .WithMany()
            .HasForeignKey(v => v.CatId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Notification relationship
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.User)
            .WithMany()
            .HasForeignKey(n => n.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Cat)
            .WithMany()
            .HasForeignKey(n => n.CatId)
            .OnDelete(DeleteBehavior.SetNull);
        
        // Indexes
        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();
        
        modelBuilder.Entity<Notification>()
            .HasIndex(n => new { n.UserId, n.Lida });
        
        modelBuilder.Entity<Finance>()
            .HasIndex(f => new { f.UserId, f.DataGasto });
        
        modelBuilder.Entity<Stock>()
            .HasIndex(s => new { s.UserId, s.Categoria });
    }
}
