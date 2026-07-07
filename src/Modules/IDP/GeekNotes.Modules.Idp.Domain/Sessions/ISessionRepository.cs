namespace GeekNotes.Modules.Idp.Domain.Sessions;

public interface ISessionRepository
{
    Task AddAsync(Session session, CancellationToken cancellationToken = default);
    Task<Session?> GetByIdAsync(SessionId id, CancellationToken cancellationToken = default);
    Task<Session?> GetByRefreshTokenHashAsync(RefreshTokenHash refreshTokenHash, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Session>> GetActiveSessionsAsync(Guid userId, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Session>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    Task RevokeAllAsync(Guid userId,CancellationToken cancellationToken);
    Task<bool> ExistsAsync(SessionId sessionId, CancellationToken cancellationToken);
    Task UpdateAsync(Session session, CancellationToken cancellationToken = default);
    Task RemoveAsync(Session session, CancellationToken cancellationToken = default);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}