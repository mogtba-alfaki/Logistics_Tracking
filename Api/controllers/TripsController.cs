using Core.Helpers;
using Core.Trips;
using Core.Trips.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.controllers;

[ApiController]
[Route("/api/v1/trips")]
public class TripsController : ControllerBase {
    private readonly TripService _tripService;

    public TripsController(TripService tripService) {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> ListTrips([FromQuery] CustomQueryParameters queryParameters) {
        var result = await _tripService.ListTrips(queryParameters);
        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> AddTrip(AddTripDto trip) {
        var result = await _tripService.AddTrip(trip);
        return Ok(result);
    }

    [HttpPost]
    [Route("location")]
    public async Task<IActionResult> UpdateTripLocation(TripLocationDto dto) {
        var result = await _tripService.UpdateTripLocation(dto);
        return Ok(result);
    }

    [HttpPost]
    [Route("end")]
    public async Task<IActionResult> EndTrip(string id) {
        var result = await _tripService.EndTrip(id);
        return Ok(result);
    }
}