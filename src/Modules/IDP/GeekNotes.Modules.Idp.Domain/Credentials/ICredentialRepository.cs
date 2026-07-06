namespace GeekNotes.Modules.Idp.Domain.Credentials;

public interface ICredentialRepository
{
    Task AddAsync(Credential credential, CancellationToken cancellationToken = default);
    Task<Credential?> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<Credential?> GetByIdAsync(CredentialId id, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Credential credential, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}
