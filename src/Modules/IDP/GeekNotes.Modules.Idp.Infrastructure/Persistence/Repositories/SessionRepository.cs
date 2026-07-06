using GeekNotes.Modules.Idp.Domain.Sessions;
using Microsoft.EntityFrameworkCore;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Repositories;

internal sealed class SessionRepository
    : ISessionRepository
{
    private readonly IdpDbContext _context;

    public SessionRepository(
        IdpDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Session session,
        CancellationToken cancellationToken = default)
    {
        await _context.Sessions.AddAsync(
            session,
            cancellationToken);
    }

    public async Task<Session?> GetByIdAsync(
        SessionId id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Sessions
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Session?> GetByRefreshTokenAsync(
        RefreshTokenHash refreshTokenHash,
        CancellationToken cancellationToken = default)
    {
        return await _context.Sessions
            .FirstOrDefaultAsync(
                x => x.RefreshTokenHash == refreshTokenHash,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Session>> GetActiveSessionsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Sessions
            .Where(x =>
                x.UserId == userId &&
                x.RevokedOnUtc == null)
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(
    Session session,
    CancellationToken cancellationToken = default)
    {
        _context.Sessions.Update(session);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(
        Session session,
        CancellationToken cancellationToken = default)
    {
        _context.Sessions.Remove(session);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
