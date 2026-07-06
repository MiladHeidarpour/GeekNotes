namespace GeekNotes.Modules.Idp.Domain.Sessions;

public interface ISessionRepository
{
    Task AddAsync(Session session, CancellationToken cancellationToken = default);
    Task<Session?> GetByIdAsync(SessionId id, CancellationToken cancellationToken = default);
    Task<Session?> GetByRefreshTokenAsync(RefreshTokenHash refreshTokenHash, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Session>> GetActiveSessionsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task UpdateAsync(Session session, CancellationToken cancellationToken = default);
    Task RemoveAsync(Session session, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}