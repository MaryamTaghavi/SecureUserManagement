using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SecureUserManagement.Data;
using SecureUserManagement.Infrustructure;
using SecureUserManagement.Interfaces;
using SecureUserManagement.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SecureUserManagement.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly RevokeRefreshToken _revokeRefreshTokens;

    public AuthService(AppDbContext context, IConfiguration configuration, RevokeRefreshToken revokeRefreshTokens)
    {
        _context = context;
        _configuration = configuration;
        _revokeRefreshTokens = revokeRefreshTokens;
    }

    public async Task<LoginResponse?> LoginWithPasswordAsync(LoginRequest login, CancellationToken cancellationToken)
    {
        var user = await _context.Users.FirstOrDefaultAsync(r => r.Name == login.UserName && r.Password == login.Password, cancellationToken);

        if (user == null)
        {
            return null;
        }

        var response = (GenerateToken(user), GenerateRefreshtoken());

        var rec = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = response.Item2,
            ExpiresOnUtc = DateTime.UtcNow.AddDays(7),

        };

        await _revokeRefreshTokens.Handle(user.Id);

        await _context.RefreshTokens.AddAsync(rec, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return new LoginResponse(response.Item1, response.Item2);
    }

    public async Task<LoginResponse?> LoginWithRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var refreshtoken = await _context.RefreshTokens
            .Include(r => r.User).FirstOrDefaultAsync(r => r.Token == refreshToken, cancellationToken);

        if (refreshtoken == null)
        {
            return null;
        }

        var response = (GenerateToken(refreshtoken.User), GenerateRefreshtoken());

        refreshtoken.Token = response.Item2;
        refreshtoken.ExpiresOnUtc = DateTime.UtcNow.AddDays(7);

        _context.RefreshTokens.Update(refreshtoken);
        await _context.SaveChangesAsync(cancellationToken);

        return new LoginResponse(response.Item1, response.Item2);
    }


    public string GenerateToken(User user)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Name),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshtoken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }
}
