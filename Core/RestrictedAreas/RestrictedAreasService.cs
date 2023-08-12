using Core.Helpers;
using Core.RestrictedAreas.Dto;
using Core.RestrictedAreas.UseCases;
using Domain.Entities;

namespace Core.RestrictedAreas; 

public class RestrictedAreasService {
    private readonly RestrictedAreasUseCases _useCases;

    public RestrictedAreasService(RestrictedAreasUseCases useCases) {
        _useCases = useCases;
    } 
    
    public async Task<List<RestrictedArea>> ListRestrictedAreas(CustomQueryParameters queryParameters) {
        return await _useCases.ListRestrictedAreas(queryParameters); 
    } 
    
    public async Task<RestrictedArea> AddRestrictedArea(AddRestrictedArea addRestrictedAreaDto) {
        return await _useCases.AddRestrictedArea(addRestrictedAreaDto); 
    }

    public async Task<bool> DeleteRestrictedArea(string id) {
        return await _useCases.DeleteRestrictedArea(id); 
    }

    public async Task<GetRestrictedAreaDto> GetRestrictedAreaById(string id) {
        return await _useCases.GetRestrictedAreaById(id); 
    }
    
}