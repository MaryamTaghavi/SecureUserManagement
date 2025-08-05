using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureUserManagement.Model;
using SecureUserManagement.Interfaces;

namespace SecureUserManagement.Controllers;

[ApiController]
[Authorize]
public class AuthController : ControllerBase
{
    private IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService; 
    }

    /// <summary>
    /// لاگین با پسورد
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("LoginWithPassword")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> LoginWithPassword([FromBody] LoginRequest login, CancellationToken cancellationToken)
    {
        var result = await _authService.LoginWithPasswordAsync(login, cancellationToken);

        if (result == null)
            return BadRequest("کاربر یافت نشد!");

        return Ok(result);
    }

    /// <summary>
    /// لاگین با رفرش توکن
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("LoginWithRefreshToken")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]

    public async Task<IActionResult> LoginWithRefreshToken([FromBody] string refreshToken , CancellationToken cancellationToken)
    {
        var result = await _authService.LoginWithRefreshTokenAsync(refreshToken, cancellationToken);

        if (result == null)
            return BadRequest("کاربر یافت نشد!");

        return Ok(result);
    }
}
