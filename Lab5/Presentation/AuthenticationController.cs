using Itmo.ObjectOrientedProgramming.Lab5.Application.Services;
using Itmo.ObjectOrientedProgramming.Lab5.Core.Records;
using Microsoft.AspNetCore.Mvc;

namespace Itmo.ObjectOrientedProgramming.Lab5.Presentation;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authService;

    public AuthenticationController(IAuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login/user")]
    public IActionResult LoginUser([FromBody] LoginUserRequest request)
    {
        (ResultType result, Guid? sessionId) = _authService.LoginUser(request.AccountId, request.Pin);
        return result is ResultType.Failure f ? BadRequest(f.Str) : (IActionResult)Ok(sessionId);
    }

    [HttpPost("login/admin")]
    public IActionResult LoginAdmin([FromBody] LoginAdminRequest request)
    {
        (ResultType result, Guid? sessionId) = _authService.LoginAdmin(request.SystemPassword);
        return result is ResultType.Failure f ? Unauthorized() : (IActionResult)Ok(sessionId);
    }
}
