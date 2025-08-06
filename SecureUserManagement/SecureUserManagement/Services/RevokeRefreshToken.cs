using Microsoft.EntityFrameworkCore;
using SecureUserManagement.Infrustructure;

namespace SecureUserManagement.Services;

public sealed class RevokeRefreshToken(AppDbContext context, IHttpContextAccessor httpContextAccessor)
{
    public async Task<bool> Handle(int userId)
    {
        await context.RefreshTokens
            .Where(r => r.UserId == userId)
            .ExecuteDeleteAsync();

        return true;
    }
}
