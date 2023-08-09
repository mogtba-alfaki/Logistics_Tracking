using Core.Trucks.Dto;
using Domain.Entities;
using Infrastructure.Helpers.AwsS3;
using Infrastructure.Repositories;
using Infrastructure.Util;
using Microsoft.AspNetCore.Http;

namespace Core.Trucks.UseCases; 

public class TruckUseCases {
    private readonly TrackingGenericRepository _repository;
    private readonly MultiPartFileHandler _multiPartFileHandler; 
    
    public TruckUseCases(TrackingGenericRepository repository, MultiPartFileHandler multiPartFileHandler) {
        _repository = repository;
        _multiPartFileHandler = multiPartFileHandler;
    }

    public async Task<List<Truck>> ListTrucks(CustomQueryParameters queryParameters) {
        return await _repository.ListTrucks(queryParameters); 
    }

    public async Task<Truck> AddTruck(TruckDto dto) {
        IFormFile truckImage = dto.TruckImage;
        var imagePath = await _multiPartFileHandler.UploadAsync(truckImage);
        var S3Id = await AwsS3Helper.UploadImageAsync(imagePath);
        File.Delete(imagePath);
        
        // TODO refactor dto mappers 
        // var truck = TruckMapper.MapDtoToEntity(truckDto);
        var truck = new Truck {
            Id = IdGenerator.Generate(),
            Color = dto.Color,
            Model = dto.Model,
            Status = dto.Status,
            ImageStorageId = S3Id,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        }; 
        return await _repository.AddTruck(truck); 
    }

    public async Task<TruckDto> GetTruckById(string id) {
        var truck = await _repository.GetTruckById(id);
        var truckDto = TruckMapper.MapEntityToDto(truck);
        return truckDto;
    }
    
    public async Task<bool> DeleteTruck(string id) {
        return await _repository.DeleteTruck(id); 
    }
}