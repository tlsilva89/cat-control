using CatControl.API.DTOs.Auth;

namespace CatControl.API.Services;

public interface IAuthService
{
    Task<AuthResponseDto?> Register(RegisterDto registerDto);
    Task<AuthResponseDto?> Login(LoginDto loginDto);
    Task<bool> EmailExists(string email);
}
