using Application.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace Web.Controllers;

[ApiController]
[Route("api/auth")]
public sealed class AuthController(
    RegisterUserUseCase registerUser,
    LoginUseCase login,
    GetCurrentUserUseCase getCurrentUser) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var result = await registerUser.ExecuteAsync(request, cancellationToken);
        return result.Succeeded ? NoContent() : BadRequest(result);
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await login.ExecuteAsync(request, cancellationToken);
        return response is null ? Unauthorized(new { message = "E-mail ou senha inválidos." }) : Ok(response);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<ActionResult<AuthenticatedUser>> Me(CancellationToken cancellationToken)
    {
        var userId = User.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        if (userId is null)
        {
            return Unauthorized();
        }

        var user = await getCurrentUser.ExecuteAsync(userId, cancellationToken);
        return user is null ? Unauthorized() : Ok(user);
    }
}
