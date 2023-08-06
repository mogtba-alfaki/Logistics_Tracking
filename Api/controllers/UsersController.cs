using Core.Users.UseCases;
using Core.Users.UseCases.Dto;
using Domain.Entities;
using Infrastructure.Util;
using Microsoft.AspNetCore.Mvc;

namespace Api.controllers; 

[ApiController]
[Route("/api/v1/users")]
public class UsersController: ControllerBase {
    private readonly UsersService _service;

    public UsersController(UsersService service) {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> ListUsers([FromQuery] CustomQueryParameters queryParameters) {
        var result = await _service.ListUsers(queryParameters);
        return Ok(result); 
    }

    [HttpPost]
    public async Task<IActionResult> Signup([FromBody] SignInDto dto) {
        var result = await _service.Signup(dto);
        return Ok(result); 
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody] UserLoginDto dto) {
        var result = await _service.Login(dto);
        return Ok(result); 
    }
}