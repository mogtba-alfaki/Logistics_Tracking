using Core.Trips;
using Core.Trips.Dto;
using Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Api.controllers; 

[ApiController] 
[Route("/api/v1/trips")]
public class TripsController: ControllerBase {
    private readonly TripService _tripService;

    public TripsController(TripService tripService) {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> ListTrips([FromQuery] CustomQueryParameters queryParameters) {
        try {
            var result = await  _tripService.ListTrips(queryParameters);
            return Ok(result);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e); 
        }
    }


    [HttpPost]
    public async Task<IActionResult> AddTrip(AddTripDto trip) {
        try {
            var result = await _tripService.AddTrip(trip);
            return Ok(result); 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e); 
        }
    }

    [HttpPost]
    [Route("location")]
    public async Task<IActionResult> UpdateTripLocation(TripLocationDto dto) {
        try {
            var result = await _tripService.UpdateTripLocation(dto);
            return Ok(result); 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e); 
        }
    }

    [HttpPost]
    [Route("end")]
    public async Task<IActionResult> EndTrip(string id) {
        try {
            var result = await _tripService.EndTrip(id);
            return Ok(result); 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e); 
        }
    }
}