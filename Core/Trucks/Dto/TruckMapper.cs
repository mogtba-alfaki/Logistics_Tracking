using Domain.Entities;
using Infrastructure.Util;

namespace Core.Trucks.Dto; 

public class TruckMapper {
    public static Truck MapDtoToEntity(TruckDto dto) {
        return new Truck {
            Id = dto.Id,
            Color = dto.Color,
            Model = dto.Model,
            Status = dto.Status,
            CreatedAt = dto.CreatedAt,
            UpdatedAt = dto.UpdatedAt,
        }; 
    }

    public static TruckDto MapEntityToDto(Truck truck) {
        return new TruckDto {
            Id = truck.Id,
            Color = truck.Color,
            Model = truck.Model,
            Status = truck.Status,
            CreatedAt = truck.CreatedAt,
            UpdatedAt = truck.UpdatedAt,
        }; 
    }
}