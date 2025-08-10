using Microsoft.AspNetCore.Authorization;
using SecureUserManagement.Authorization;
using System.Security.Claims;

namespace SecureUserManagement.Infrustructure.Authentication;

public class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirment>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory ;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirment requirment)
    {
        var claims = context.User.Claims.ToList();

        string? userId = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

        if (!int.TryParse(userId, out int parsedUserId))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();
        IPermissionService permissionService = scope.ServiceProvider.GetService<IPermissionService>();

        HashSet<string> permissions = await permissionService.GetPermissionsAsync(1);

        if (permissions.Contains(requirment.Permission))
        {
            context.Succeed(requirment);
        }
    }
}
