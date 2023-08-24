using Core.Helpers;
using Core.Interfaces;
using Core.Repositories;
using Core.RestrictedAreas.Dto;
using Core.Trucks.Dto;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Core.Trucks.UseCases; 

public class TruckUseCases {
    private readonly ITruckRepository _repository;
    private readonly IObjectStorageProvider _awsS3;
    private readonly TrucksMapper _mapper;

    public TruckUseCases(ITruckRepository repository, IObjectStorageProvider awsS3, TrucksMapper mapper) {
    private readonly ILogger _logger;
    private const string LOGPREFIX = "TruckUseCases"; 

    public TruckUseCases(ITruckRepository repository, IObjectStorageProvider awsS3, ILogger logger) {
        _repository = repository;
        _awsS3 = awsS3;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<List<TruckDto>> ListTrucks(CustomQueryParameters queryParameters) {
        _logger.LogInfo($"{LOGPREFIX}, ListTrucks, query: {queryParameters}");
        var trucks = await _repository.List(queryParameters);
        return _mapper.MapList(trucks); 
    }

    public async Task<TruckDto> AddTruck(TruckDto dto) {
        _logger.LogInfo($"{LOGPREFIX}, AddTruck, truckDto: {dto}");
        IFormFile truckImage = dto.TruckImage;
        var imagePath = await MultiPartFileHandler.UploadAsync(truckImage);
        var S3Id = await _awsS3.UploadImageAsync(imagePath);
        File.Delete(imagePath);
        
        var truck = new Truck {
            Id = IdGenerator.Generate(),
            Color = dto.Color,
            Model = dto.Model,
            Status = dto.Status,
            ImageStorageId = S3Id,
            CreatedAt = DateTime.Now.ToUniversalTime(),
            UpdatedAt = DateTime.Now.ToUniversalTime(),
        }; 
        var createdTruck = await _repository.Create(truck);
        return _mapper.MapEntityToDto(createdTruck); 
    }

    public async Task<TruckDto> GetTruckById(string id) {
        var truck = await _repository.GetById(id);
        return _mapper.MapEntityToDto(truck); 
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