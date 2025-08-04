using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureUserManagement.Data;
using SecureUserManagement.Infrustructure;
using SecureUserManagement.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using SecureUserManagement.Model;

namespace SecureUserManagement.Services; 

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(AppDbContext context , IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<string> LoginWithPasswordAsync(LoginRequest login, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(r => r.Name == login.UserName && r.Password == login.Password , cancellationToken);

        if (user == null)
        {
            return "";
        }

        return GenerateToken(user);
    }

    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
