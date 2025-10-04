using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CatControl.API.Utils;

public class JwtHelper
{
    private readonly IConfiguration _configuration;
    
    public JwtHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public string GenerateToken(int userId, string email, string nome)
    {
        var jwtKey = _configuration["Jwt:Key"] 
            ?? throw new InvalidOperationException("Jwt:Key não configurado no appsettings.json");
        var issuer = _configuration["Jwt:Issuer"] 
            ?? throw new InvalidOperationException("Jwt:Issuer não configurado no appsettings.json");
        var audience = _configuration["Jwt:Audience"] 
            ?? throw new InvalidOperationException("Jwt:Audience não configurado no appsettings.json");
        var expiryHours = int.Parse(_configuration["Jwt:ExpiryInHours"] ?? "24");
        
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(ClaimTypes.Email, email),
            new Claim(ClaimTypes.Name, nome),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };
        
        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(expiryHours),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
