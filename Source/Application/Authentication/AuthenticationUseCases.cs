namespace Application.Authentication;

public sealed class RegisterUserUseCase(IAuthenticationService authenticationService)
{
    public Task<RegistrationResult> ExecuteAsync(RegisterUserRequest request, CancellationToken cancellationToken = default) =>
        authenticationService.RegisterAsync(request, cancellationToken);
}

public sealed class LoginUseCase(IAuthenticationService authenticationService)
{
    public Task<AuthResponse?> ExecuteAsync(LoginRequest request, CancellationToken cancellationToken = default) =>
        authenticationService.LoginAsync(request, cancellationToken);
}

public sealed class GetCurrentUserUseCase(IAuthenticationService authenticationService)
{
    public Task<AuthenticatedUser?> ExecuteAsync(string userId, CancellationToken cancellationToken = default) =>
        authenticationService.GetUserAsync(userId, cancellationToken);
}
