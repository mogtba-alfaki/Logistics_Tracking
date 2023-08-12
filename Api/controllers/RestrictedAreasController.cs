using Core.RestrictedAreas;
using Core.RestrictedAreas.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Api.controllers;

[ApiController]
[Route("/api/v1/restricted_areas")]
public class RestrictedAreasController : ControllerBase {
    private readonly RestrictedAreasService _service;

    public RestrictedAreasController(RestrictedAreasService service) {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ListRestrictedAreas([FromQuery] CustomQueryParameters queryParameters) {
            var result = await _service.ListRestrictedAreas(queryParameters);
            return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddRestrictedArea([FromBody] AddRestrictedArea addRestrictedAreaDto) {
            var result = await _service.AddRestrictedArea(addRestrictedAreaDto);
            return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteRestrictedArea([FromBody] string id) {
            var result = await _service.DeleteRestrictedArea(id);
            return Ok(result);
    }

    [HttpGet]
    [Route("area")]
    public async Task<IActionResult> GetRestrictedAreaById([FromQuery] string id) {
            var result = await _service.GetRestrictedAreaById(id);
            return Ok(result); 
        }
}