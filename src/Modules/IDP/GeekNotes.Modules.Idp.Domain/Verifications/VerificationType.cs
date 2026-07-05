namespace GeekNotes.Modules.Idp.Domain.Verifications;

public enum VerificationType
{
    Email = 1,
    Phone = 2,
    PasswordReset = 3,
    ChangeEmail = 4,
    ChangePhone = 5,
    TwoFactor = 6
}