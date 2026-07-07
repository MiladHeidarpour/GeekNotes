namespace GeekNotes.Modules.Idp.Contracts.Authorize;

public sealed record AuthorizeResponse(
    string Code,
    string State);
