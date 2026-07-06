namespace GeekNotes.Modules.Idp.Contracts.Requests;

public sealed record LoginRequest(
    string Email,
    string Password);
public sealed record LoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresOnUtc);

public sealed record RefreshTokenRequest(
    string RefreshToken);

public sealed record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);

public sealed record RegisterResponse(
    Guid UserId,
    string Email);

public sealed record VerifyEmailRequest(
    string Email,
    string Code);

public sealed record VerifyPhoneRequest(
    string PhoneNumber,
    string Code);

public sealed record LogoutRequest(
    string RefreshToken);

public sealed record MeResponse(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    bool EmailConfirmed,
    bool PhoneConfirmed,
    IEnumerable<string> Roles,
    IEnumerable<string> Permissions);

public sealed record ExternalLoginRequest(
    ExternalProvider Provider,
    string Token);

public sealed record ExternalLoginResponse(
    string AccessToken,
    string RefreshToken,
    DateTime ExpiresOnUtc,
    bool RequiresPhoneNumber,
    bool RequiresEmailVerification);

public sealed record AuthorizeRequest(
    string ClientId,
    string RedirectUri,
    string ResponseType,
    string Scope,
    string State,
    string? CodeChallenge,
    string? CodeChallengeMethod);

public sealed record AuthorizeResponse(
    string Code,
    string State);


public sealed record TokenRequest(
    string GrantType,
    string ClientId,
    string? ClientSecret,
    string? Code,
    string? RedirectUri,
    string? CodeVerifier,
    string? RefreshToken);

public sealed record TokenResponse(
    string AccessToken,
    string RefreshToken,
    string TokenType,
    int ExpiresIn);


public sealed record RevokeTokenRequest(
    string Token);


public sealed record IntrospectionRequest(
    string Token);

public sealed record IntrospectionResponse(
    bool Active,
    Guid? UserId,
    string? ClientId,
    DateTime? ExpiresOnUtc,
    IEnumerable<string>? Scopes);



public enum ExternalProvider
{
    GitHub = 1,
    Google = 2,
    Apple = 3,
}