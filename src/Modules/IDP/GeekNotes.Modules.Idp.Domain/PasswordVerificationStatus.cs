namespace GeekNotes.Modules.Idp.Domain;

public enum PasswordVerificationStatus
{
    Failed = 0,
    Success = 1,
    SuccessRehashNeeded = 2
}
