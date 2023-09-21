using Core.Trips.Dto;
using Core.Trips.UseCases;

namespace Worker.Services; 

public class LocationServices {
    private readonly UpdateTripLocationUseCase _updateTripLocationUseCase;

    public LocationServices(UpdateTripLocationUseCase updateTripLocationUseCase) {
        _updateTripLocationUseCase = updateTripLocationUseCase;
    }

    public async Task UpdateLocation(TripLocationDto dto) {
        await _updateTripLocationUseCase.Update(dto); 
    }
}