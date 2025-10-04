using Microsoft.EntityFrameworkCore;
using CatControl.API.Data;
using CatControl.API.DTOs.Auth;
using CatControl.API.Models;
using CatControl.API.Utils;

namespace CatControl.API.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly JwtHelper _jwtHelper;
    
    public AuthService(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _jwtHelper = new JwtHelper(configuration);
    }
    
    public async Task<AuthResponseDto?> Register(RegisterDto registerDto)
    {
        if (await EmailExists(registerDto.Email))
        {
            return null;
        }
        
        var user = new User
        {
            Nome = registerDto.Nome,
            Email = registerDto.Email.ToLower(),
            PasswordHash = PasswordHelper.HashPassword(registerDto.Password),
            CreatedAt = DateTime.UtcNow
        };
        
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        
        var token = _jwtHelper.GenerateToken(user.Id, user.Email, user.Nome);
        
        return new AuthResponseDto
        {
            Token = token,
            UserId = user.Id,
            Nome = user.Nome,
            Email = user.Email
        };
    }
    
    public async Task<AuthResponseDto?> Login(LoginDto loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == loginDto.Email.ToLower());
        
        if (user == null)
        {
            return null;
        }
        
        if (!PasswordHelper.VerifyPassword(loginDto.Password, user.PasswordHash))
        {
            return null;
        }
        
        var token = _jwtHelper.GenerateToken(user.Id, user.Email, user.Nome);
        
        return new AuthResponseDto
        {
            Token = token,
            UserId = user.Id,
            Nome = user.Nome,
            Email = user.Email
        };
    }
    
    public async Task<bool> EmailExists(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email.ToLower());
    }
}
