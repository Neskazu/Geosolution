using CityInfo.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<UserModel>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options) {}
    public DbSet<CityObjectModel> CityObjects { get; set; }
    public DbSet<CityObjectType> CityObjectTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Database=CityInfo;Username=postgres;Password=qaz741");
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CityModelTypeMapping>()
            .HasKey(c => new { c.CityObjectModelId, c.CityObjectTypeId });
    }
}