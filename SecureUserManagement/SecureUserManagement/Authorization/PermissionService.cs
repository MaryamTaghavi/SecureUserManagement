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
         ICollection<RolePermission>[] roles = await _context.Set<User>()
            .Include(r => r.RolePermissions)
            .ThenInclude(r => r.Permission)
            .Include(r => r.RolePermissions)
            .ThenInclude(r => r.Role)
            .Where(x => x.Id == userId)
            .Select(x => x.RolePermissions)
            .ToArrayAsync();

        return roles.SelectMany(x => x)
            .SelectMany(x => x.Role.Permissions)
            .Select(x => x.Name)
            .ToHashSet();
    }
}
