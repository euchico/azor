namespace Application.Authentication;

public sealed record RegisterUserRequest(string Email, string Password);

public sealed record LoginRequest(string Email, string Password);

public sealed record AuthenticatedUser(string Id, string Email);

public sealed record AuthResponse(string AccessToken, DateTimeOffset ExpiresAt, AuthenticatedUser User);

public sealed record RegistrationResult(bool Succeeded, IReadOnlyCollection<string> Errors)
{
    public static RegistrationResult Success() => new(true, []);
}
