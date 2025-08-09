using SecureUserManagement.Authorization;

namespace SecureUserManagement.Data; 

public class User
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }

    public ICollection<RolePermission> RolePermissions { get; set; }
    public ICollection<UserRole> UserRoles { get; set; }

}
