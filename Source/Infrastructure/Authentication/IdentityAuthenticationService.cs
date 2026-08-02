using Application.Authentication;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Authentication;

public sealed class IdentityAuthenticationService(
    UserManager<ApplicationUser> userManager,
    IJwtTokenGenerator tokenGenerator) : IAuthenticationService
{
    public async Task<RegistrationResult> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = await userManager.CreateAsync(user, request.Password);

        return result.Succeeded
            ? RegistrationResult.Success()
            : new RegistrationResult(false, result.Errors.Select(error => error.Description).ToArray());
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByEmailAsync(request.Email);
        if (user is null)
        {
            return null;
        }

        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);
        return isPasswordValid ? tokenGenerator.Create(user) : null;
    }

    public async Task<AuthenticatedUser?> GetUserAsync(string userId, CancellationToken cancellationToken = default)
    {
        var user = await userManager.FindByIdAsync(userId);
        return user is null ? null : new AuthenticatedUser(user.Id, user.Email!);
    }
}
