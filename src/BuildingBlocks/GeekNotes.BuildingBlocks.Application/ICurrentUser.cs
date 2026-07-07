using System.Security.Claims;

namespace GeekNotes.BuildingBlocks.Application;

public interface ICurrentUser
{
    bool IsAuthenticated { get; }

    Guid UserId { get; }

    Guid SessionId { get; }

    string? Email { get; }

    IReadOnlyCollection<string> Roles { get; }

    IReadOnlyCollection<Claim> Claims { get; }

    bool IsInRole(string role);

    bool HasClaim(string type, string value);
}
