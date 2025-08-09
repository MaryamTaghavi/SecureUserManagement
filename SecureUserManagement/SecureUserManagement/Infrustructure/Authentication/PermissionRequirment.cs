using Microsoft.AspNetCore.Authorization;

namespace SecureUserManagement.Infrustructure.Authentication; 

public class PermissionRequirment : IAuthorizationRequirement
{
    public PermissionRequirment(string permission)
    {
        Permission = permission;
    }
    public string Permission { get; set; }
}
