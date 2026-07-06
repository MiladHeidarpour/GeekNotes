using GeekNotes.Modules.Idp.Domain.ExternalLogins;
using Microsoft.EntityFrameworkCore;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Repositories;

internal sealed class ExternalLoginRepository
    : IExternalLoginRepository
{
    private readonly IdpDbContext _context;

    public ExternalLoginRepository(
        IdpDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        ExternalLogin externalLogin,
        CancellationToken cancellationToken = default)
    {
        await _context.ExternalLogins.AddAsync(
            externalLogin,
            cancellationToken);
    }

    public async Task<ExternalLogin?> GetByIdAsync(
        ExternalLoginId id,
        CancellationToken cancellationToken = default)
    {
        return await _context.ExternalLogins
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<ExternalLogin?> GetAsync(
        ExternalProvider provider,
        string providerUserId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ExternalLogins
            .FirstOrDefaultAsync(
                x =>
                    x.Provider == provider &&
                    x.ProviderUserId == providerUserId,
                cancellationToken);
    }

    public async Task<IReadOnlyList<ExternalLogin>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ExternalLogins
            .Where(x => x.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        ExternalProvider provider,
        string providerUserId,
        CancellationToken cancellationToken = default)
    {
        return await _context.ExternalLogins
            .AnyAsync(
                x =>
                    x.Provider == provider &&
                    x.ProviderUserId == providerUserId,
                cancellationToken);
    }


    public Task UpdateAsync(
    ExternalLogin external,
    CancellationToken cancellationToken = default)
    {
        _context.ExternalLogins.Update(external);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(
        ExternalLogin external,
        CancellationToken cancellationToken = default)
    {
        _context.ExternalLogins.Remove(external);

        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}