using Core.Helpers;
using Core.Trucks;
using Core.Trucks.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.controllers; 

[ApiController]
[Route("api/v1/trucks")]
public class TrucksController: ControllerBase {
    private readonly TrucksService _service;
    
    public TrucksController(TrucksService service) {
        _service = service;
    }
    public async Task<IActionResult> AddTruck([FromForm] TruckDto dto) {
            var result = await _service.AddTruck(dto);
            return Ok(result);
        }
    
    [HttpGet]
    public async Task<IActionResult> ListTrucks([FromQuery] CustomQueryParameters queryParameters) {
            var result = await _service.ListTrucks(queryParameters);
            return Ok(result);
    }


    // TODO: CHECK THE DELETE METHOD IN ALL CONTROLLERS
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTruck(string id) {
            var result = await _service.DeleteTruck(id);
            return Ok(result);
        }

    [HttpGet]
    [Route("truck")]
    public async Task<IActionResult> GetTruckById([FromQuery] string id) {
            var result = await _service.GetTruckById(id);
            return Ok(result);
        }
}