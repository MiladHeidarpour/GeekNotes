namespace GeekNotes.Modules.Idp.Contracts.VerifyPhone;

public sealed record VerifyPhoneRequest(
    string PhoneNumber,
    string Code);
