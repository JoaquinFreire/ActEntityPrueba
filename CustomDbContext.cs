using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

public class CustomDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Booking> Bookings { get; set; }
    public DbSet<Destination> Destinations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();
        
        var connectionString = config.GetConnectionString("DefaultConnection");
        optionsBuilder.UseMySQL(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Customer>().HasMany(c => c.Bookings).WithOne(b => b.Customer).HasForeignKey(b => b.CustomerId);
        modelBuilder.Entity<Destination>().HasMany(d => d.Bookings).WithOne(b => b.Destination).HasForeignKey(b => b.DestinationId);
    }
}
