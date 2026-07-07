namespace GeekNotes.Modules.Idp.Domain;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(
        Guid userId,
        Guid sessionId,
        string email,
        IEnumerable<string> roles,
        IEnumerable<string> permissions);
}
