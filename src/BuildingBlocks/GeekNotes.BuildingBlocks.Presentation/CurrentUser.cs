using System.Security.Claims;

namespace GeekNotes.BuildingBlocks.Application;

public sealed class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private ClaimsPrincipal User =>
        _httpContextAccessor.HttpContext?.User
        ?? new ClaimsPrincipal();

    public bool IsAuthenticated =>
        User.Identity?.IsAuthenticated ?? false;

    public Guid UserId
    {
        get
        {
            var value =
                User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue(ClaimTypes.Name)
                ?? User.FindFirstValue("sub");

            if (Guid.TryParse(value, out var id))
                return id;

            return Guid.Empty;
        }
    }

    public Guid SessionId
    {
        get
        {
            var value = User.FindFirstValue("sid");

            return Guid.TryParse(value, out var id)
                ? id
                : Guid.Empty;
        }
    }
    public string? Email =>
        User.FindFirstValue(ClaimTypes.Email);

    public IReadOnlyCollection<string> Roles =>
        User.FindAll(ClaimTypes.Role)
            .Select(x => x.Value)
            .ToList();

    public IReadOnlyCollection<Claim> Claims =>
        User.Claims.ToList();

    public bool IsInRole(string role)
        => User.IsInRole(role);

    public bool HasClaim(string type, string value)
        => User.HasClaim(type, value);
}