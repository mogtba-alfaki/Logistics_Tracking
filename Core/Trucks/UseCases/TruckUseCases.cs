using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Core.Trucks.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Trucks.UseCases; 

public class TruckUseCases {
    private readonly ITruckRepository _repository;
    private readonly IObjectStorageProvider _awsS3;
    private readonly ILogger _logger;
    private const string LOGPREFIX = "TruckUseCases"; 

    public TruckUseCases(ITruckRepository repository, IObjectStorageProvider awsS3, ILogger logger) {
        _repository = repository;
        _awsS3 = awsS3;
        _logger = logger;
    }

    public async Task<List<Truck>> ListTrucks(CustomQueryParameters queryParameters) {
        _logger.LogInfo($"{LOGPREFIX}, ListTrucks, query: {queryParameters}");
        return await _repository.List(queryParameters); 
    }

    public async Task<Truck> AddTruck(TruckDto dto) {
        _logger.LogInfo($"{LOGPREFIX}, AddTruck, truckDto: {dto}");
        IFormFile truckImage = dto.TruckImage;
        var imagePath = await MultiPartFileHandler.UploadAsync(truckImage);
        var S3Id = await _awsS3.UploadImageAsync(imagePath);
        File.Delete(imagePath);
        
        // TODO refactor dto mappers 
        // var truck = Truck
        // Mapper.MapDtoToEntity(truckDto);
        var truck = new Truck {
            Id = IdGenerator.Generate(),
            Color = dto.Color,
            Model = dto.Model,
            Status = dto.Status,
            ImageStorageId = S3Id,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        }; 
        return await _repository.Create(truck); 
    }

    public async Task<TruckDto> GetTruckById(string id) {
        var truck = await _repository.GetById(id);
        var truckDto = TruckMapper.MapEntityToDto(truck);
        return truckDto;
    }

    public async Task<TruckDto> GetTruckProfile(string id) {
        _logger.LogInfo($"{LOGPREFIX}, GetTruckProfile, TruckId: {id}");
        var truck = await _repository.GetById(id);
        var truckImage = await _awsS3.GetImageAsync(truck.ImageStorageId);
        return new TruckDto {
            Id = truck.Id,
            TruckImage = (IFormFile) truckImage,
            Status = truck.Status,
            Color = truck.Color,
            Model = truck.Model,
            ImageStorageId = truck.ImageStorageId,
            CreatedAt = truck.CreatedAt,
            UpdatedAt = truck.UpdatedAt,
        };
    }
    
    public async Task<bool> DeleteTruck(string id) {
        _logger.LogInfo($"{LOGPREFIX}, DeleteTruck, TruckId: {id}");
        return await _repository.Delete(id); 
    }
}