using SecureUserManagement.Infrustructure;
using SecureUserManagement.Data;
using Microsoft.EntityFrameworkCore;

namespace SecureUserManagement.Authorization;

public class PermissionService : IPermissionService
{
    private readonly AppDbContext _context;

    public PermissionService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionsAsync(int userId)
    {
         ICollection<UserRole>[] roles = await _context.Set<User>()
            .Include(r => r.UserRoles)
            .ThenInclude(r => r.Role)
            .ThenInclude(r => r.Permissions)
            .Where(x => x.Id == userId)
            .Select(x => x.UserRoles)
            .ToArrayAsync();

        return roles.SelectMany(x => x)
            .SelectMany(x => x.Role.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
