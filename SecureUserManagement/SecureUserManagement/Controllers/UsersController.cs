using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecureUserManagement.Controllers; 

public class UsersController : Controller
{
    /// <summary>
    /// لاگین با پسورد
    /// </summary>
    /// <param name="payload"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    //[HttpPost("LoginWithPassword")]
    //[Authorize]
    //[ProducesResponseType(typeof(Result<RefreshTokenViewModel?>), StatusCodes.Status200OK)]
    //public async Task<IActionResult> LoginWithPassword([FromBody] LoginWithPasswordPayload payload, CancellationToken cancellationToken)
    //{
    //    var command = payload.ToLoginWithPasswordCommand();
    //    var result = await _mediatr.Send(command, cancellationToken);
    //    return FromResult(result);
    //}
}
