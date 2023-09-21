using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Database; 

public class TrackingContext: DbContext{
    public TrackingContext() {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Truck>() 
            .ToTable("trucks").HasKey(truck => truck.Id);


        modelBuilder.Entity<Trip>()
            .ToTable("trips")
            .HasKey(trip => trip.Id);

        modelBuilder.Entity<Trip>()
            .HasOne(x => x.Shipment)
            .WithOne(x => x.Trip)
            .HasForeignKey<Trip>(x => x.ShipmentId)
            .IsRequired();

        modelBuilder.Entity<Trip>()
            .HasMany(x => x.RestrictedAreas)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId)
            .IsRequired(); 
        
        modelBuilder.Entity<Trip>()
            .HasMany(x => x.TripLocations)
            .WithOne(x => x.Trip)
            .HasForeignKey(x => x.TripId)
            .IsRequired(); 
        
        modelBuilder.Entity<Shipment>()
            .ToTable("shipments").HasKey(shipment => shipment.Id);
        
        modelBuilder.Entity<RestrictedArea>()
            .ToTable("restricted_areas").HasKey(area => area.Id);
        
        modelBuilder.Entity<TripLocation>()
            .ToTable("trip_locations").HasKey(location => location.Id);

        modelBuilder.Entity<User>()
            .ToTable("users").HasKey(user => user.Id);
        modelBuilder.Entity<User>(); 

        // TODO MAKE USERNAME UNIQUE 
        // modelBuilder.Entity<User>()
            // .Property<string>(x => x.Username).

    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var DB_USER = Environment.GetEnvironmentVariable("DB_USER");
        var DB_PASSWORD = Environment.GetEnvironmentVariable("DB_PASSWORD");
        var PORT = Environment.GetEnvironmentVariable("PORT");
        var DATABASE = Environment.GetEnvironmentVariable("DATABASE");
        var HOST = Environment.GetEnvironmentVariable("HOST"); 
        
        var connectionString = 
            $"User Id={DB_USER}; Password={DB_PASSWORD};" +
            $" Host={HOST}; Port={PORT};Database={DATABASE}; Pooling=true;";
        optionsBuilder.UseNpgsql(connectionString).LogTo(Console.WriteLine, LogLevel.Information); 
    }

    public DbSet<Truck> Trucks { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<RestrictedArea> RestrictedAreas { get; set; }
    public DbSet<TripLocation> TripLocations { get; set; }     
    
    public DbSet<User> Users { get; set; }
}