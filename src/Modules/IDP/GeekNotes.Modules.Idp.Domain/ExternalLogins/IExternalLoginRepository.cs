namespace GeekNotes.Modules.Idp.Domain.ExternalLogins;

public interface IExternalLoginRepository
{
    Task AddAsync(ExternalLogin externalLogin, CancellationToken cancellationToken = default);
    Task<ExternalLogin?> GetByIdAsync(ExternalLoginId id, CancellationToken cancellationToken = default);
    Task<ExternalLogin?> GetAsync(ExternalProvider provider, string providerUserId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<ExternalLogin>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<bool> ExistsAsync(ExternalProvider provider, string providerUserId, CancellationToken cancellationToken = default);
    Task UpdateAsync(ExternalLogin externalLogin, CancellationToken cancellationToken = default);
    Task RemoveAsync(ExternalLogin externalLogin, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}