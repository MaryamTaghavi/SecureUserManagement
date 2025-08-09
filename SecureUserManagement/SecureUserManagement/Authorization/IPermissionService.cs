namespace SecureUserManagement.Authorization; 

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionsAsync(int userId);
}
