using Microsoft.EntityFrameworkCore;
using Domain.Entities;

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
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        var connectionString = "User Id=postgres; Password=postgres; Host=172.17.0.3; Port=5432;Database=tracking; Pooling=true;";
        optionsBuilder.UseNpgsql(connectionString);
    }

    public DbSet<Truck> Trucks { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<RestrictedArea> RestrictedAreas { get; set; }
    public DbSet<TripLocation> TripLocations { get; set; }     
}