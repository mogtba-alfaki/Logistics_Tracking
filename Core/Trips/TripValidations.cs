using System.Data;
using Domain.Entities;
using FluentValidation;

namespace Core.Trips; 

public class TripValidations: AbstractValidator<Trip> {
    public TripValidations() {
        RuleForEach(trip => trip.ShipmentId)
            .NotNull();
        RuleFor(trip => trip.TruckId)
            .NotNull(); 
    }
}