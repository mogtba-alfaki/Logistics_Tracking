using Core.Trucks;
using Core.Trucks.Dto;
using Domain.Entities;
using Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Api.controllers; 

[ApiController]
[Route("api/v1/trucks")]
public class TrucksController: ControllerBase {
    private readonly TrucksService _service;

    public async Task<IActionResult> AddTruck([FromBody] TruckDto dto) {
        try {
            var result = await _service.AddTruck(dto);
            return Ok(result); 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e); 
        }
    }

    public TrucksController(TrucksService service) {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ListTrucks([FromQuery] CustomQueryParameters queryParameters) {
        try {
            var result = await _service.ListTrucks(queryParameters);
            return Ok(result);
        }
        catch (Exception e) {
            Console.WriteLine(e);
            throw;
        }
    }


    // TODO: CHECK THE DELETE METHOD IN ALL CONTROLLERS
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTruck(string id) {
        try {
            var result = await _service.DeleteTruck(id);
            return Ok(result); 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e);
        }
    }

    [HttpGet]
    [Route("truck")]
    public async Task<IActionResult> GetTruckById([FromQuery] string id) {
        try {
            var result = await _service.GetTruckById(id);
            return Ok(result); 
        }
        catch (Exception e) {
            Console.WriteLine(e);
            return BadRequest(e);
        }
    }
}