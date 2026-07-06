namespace GeekNotes.Modules.Idp.Application.Authentications.Me;

public sealed record MeQueryResponse(
    Guid Id,
    string Email,
    string PhoneNumber,
    string UserName,
    string FullName,
    string Avatar,
    DateTime JoinedOnUtc,
    IReadOnlyCollection<string> Roles);