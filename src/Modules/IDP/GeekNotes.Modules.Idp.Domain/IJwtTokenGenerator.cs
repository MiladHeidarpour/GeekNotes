namespace GeekNotes.Modules.Idp.Domain;

public interface IJwtTokenGenerator
{
    string GenerateAccessToken(
        Guid userId,
        string email,
        IEnumerable<string> roles,
        IEnumerable<string> permissions);
}
