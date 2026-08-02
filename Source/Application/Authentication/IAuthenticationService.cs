namespace Application.Authentication;

public interface IAuthenticationService
{
    Task<RegistrationResult> RegisterAsync(RegisterUserRequest request, CancellationToken cancellationToken = default);

    Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    Task<AuthenticatedUser?> GetUserAsync(string userId, CancellationToken cancellationToken = default);
}
