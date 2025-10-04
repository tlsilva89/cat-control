using Microsoft.AspNetCore.Mvc;
using CatControl.API.DTOs.Auth;
using CatControl.API.Services;

namespace CatControl.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _authService.Register(registerDto);
        
        if (result == null)
        {
            return BadRequest(new { message = "Email já está em uso" });
        }
        
        return Ok(result);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        var result = await _authService.Login(loginDto);
        
        if (result == null)
        {
            return Unauthorized(new { message = "Email ou senha inválidos" });
        }
        
        return Ok(result);
    }
    
    [HttpGet("check-email")]
    public async Task<IActionResult> CheckEmail([FromQuery] string email)
    {
        var exists = await _authService.EmailExists(email);
        return Ok(new { exists });
    }
}
