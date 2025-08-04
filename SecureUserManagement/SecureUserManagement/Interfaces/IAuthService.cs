using SecureUserManagement.Model;

namespace SecureUserManagement.Interfaces; 

public interface IAuthService
{
    Task<string> LoginWithPasswordAsync(LoginRequest login , CancellationToken cancellationToken);
}
