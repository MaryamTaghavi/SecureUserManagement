using SecureUserManagement.Model;

namespace SecureUserManagement.Interfaces; 

public interface IAuthService
{
    Task<LoginResponse?> LoginWithPasswordAsync(LoginRequest login , CancellationToken cancellationToken);
    Task<LoginResponse?> LoginWithRefreshTokenAsync(string refreshToken, CancellationToken cancellationToken);

}
