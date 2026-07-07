namespace GeekNotes.Modules.Idp.Contracts.Me;

public sealed record MeResponse(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    bool EmailConfirmed,
    bool PhoneConfirmed,
    IEnumerable<string> Roles,
    IEnumerable<string> Permissions);
