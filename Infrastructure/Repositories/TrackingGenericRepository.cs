using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Util;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories; 

public class TrackingGenericRepository {
    private readonly TrackingContext _trackingContext;

    public TrackingGenericRepository(TrackingContext trackingContext) {
        _trackingContext = trackingContext;
    } 
    
    public async Task<List<RestrictedArea>> ListRestrictedAreas(CustomQueryParameters options) {
        var pageNumber = options.PageNumber;
        var pageSize = options.PageSize;
        var offset = pageNumber * pageSize;
        var result = await _trackingContext.RestrictedAreas
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();
        return result;
    }

    public async Task<RestrictedArea> AddRestrictedArea(RestrictedArea restrictedArea) {
        var entry =  await _trackingContext.RestrictedAreas.AddAsync(restrictedArea);
        await _trackingContext.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<bool> DeleteRestrictedArea(string id) {
        var entry = await _trackingContext.RestrictedAreas.FindAsync(id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }

        _trackingContext.RestrictedAreas.Remove(entry);
        await _trackingContext.SaveChangesAsync();
        return true; 
    }

    public async Task<RestrictedArea> GetRestrictedAreaById(string id) {
        var entry = await _trackingContext.RestrictedAreas.FindAsync(id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }
        return entry; 
    }
    
    public async Task<List<Truck>> ListTrucks(CustomQueryParameters options) {
        var pageNumber = options.PageNumber;
        var pageSize = options.PageSize;
        var offset = pageNumber * pageSize;
        var result = await _trackingContext.Trucks
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();
        return result;
    }

    public async Task<Truck> GetTruckById(string id) {
        var entry = await _trackingContext.Trucks.FindAsync(id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }
        return entry; 
    }

    public async Task<Truck> AddTruck(Truck truck) {
        var entry =  await _trackingContext.Trucks.AddAsync(truck);
        await _trackingContext.SaveChangesAsync();
        return entry.Entity;
    } 
    
    public async Task<bool> DeleteTruck(string id) {
        var entry = await _trackingContext.Trucks.FindAsync(id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }

        _trackingContext.Trucks.Remove(entry);
        await _trackingContext.SaveChangesAsync();
        return true; 
    }

    public async Task<bool> ChangeTruckStatus(string truckId, int newStatus) {
        var truck = await GetTruckById(truckId);
        truck.Status = newStatus;
        _trackingContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<List<Shipment>> ListShipments(CustomQueryParameters options) {
        var pageNumber = options.PageNumber;
        var pageSize = options.PageSize;
        var offset = pageNumber * pageSize;
        var result = await _trackingContext.Shipments
            .Skip(offset)
            .Take(pageSize)
            .ToListAsync();
        return result;
    }

    public async Task<Shipment> GetShipmentById(string id) {
        var entry = await _trackingContext.Shipments.FindAsync(id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }
        return entry; 
    }

    public async Task<Shipment> AddShipment(Shipment shipment) {
        var entry =  await _trackingContext.Shipments.AddAsync(shipment);
        await _trackingContext.SaveChangesAsync();
        return entry.Entity;
    } 
    
    public async Task<bool> DeleteShipment(string id) {
        var entry = await _trackingContext.Shipments.FindAsync(id);
        if (entry is null) {
            throw new Exception("Not Found"); 
        }

        _trackingContext.Shipments.Remove(entry);
        await _trackingContext.SaveChangesAsync();
        return true; 
    }
    
}