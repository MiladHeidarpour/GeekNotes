namespace GeekNotes.Modules.Idp.Contracts.Register;

public sealed record RegisterRequest(
    string Email,
    string Password,
    string FirstName,
    string LastName);
