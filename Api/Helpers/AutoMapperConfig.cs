using AutoMapper;
using Core.RestrictedAreas.Dto;
using Core.Trips.Dto;
using Core.Trucks.Dto;
using Core.Users.UseCases.Dto;
using Domain.Entities;

namespace Api.Helpers; 

public class AutoMapperConfig: Profile {
    public AutoMapperConfig() {
        CreateMap<AddTripDto, Trip>().ReverseMap();
        CreateMap<TripLocationDto, TripLocation>().ReverseMap();
        
        CreateMap<TruckDto, Truck>().ReverseMap(); 
        
        CreateMap<ShipmentDto, Shipment>().ReverseMap();

        CreateMap<AddRestrictedArea, RestrictedArea>().ReverseMap();
        CreateMap<GetRestrictedAreaDto, RestrictedArea>().ReverseMap();

        CreateMap<SignInDto, User>().ReverseMap();
        CreateMap<UserLoginDto, User>().ReverseMap(); 
    }
}