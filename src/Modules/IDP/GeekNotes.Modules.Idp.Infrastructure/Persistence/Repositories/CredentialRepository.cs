using GeekNotes.Modules.Idp.Domain.Credentials;
using Microsoft.EntityFrameworkCore;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Repositories;

internal sealed class CredentialRepository : ICredentialRepository
{
    private readonly IdpDbContext _context;

    public CredentialRepository(
        IdpDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Credential credential,
        CancellationToken cancellationToken = default)
    {
        await _context.Credentials.AddAsync(
            credential,
            cancellationToken);
    }

    public async Task<Credential?> GetByIdAsync(
        CredentialId id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Credentials
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Credential?> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Credentials
            .FirstOrDefaultAsync(
                x => x.UserId == userId,
                cancellationToken);
    }

    public async Task<bool> ExistsAsync(
        Guid userId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Credentials
            .AnyAsync(
                x => x.UserId == userId,
                cancellationToken);
    }

    public Task UpdateAsync(
        Credential credential,
        CancellationToken cancellationToken = default)
    {
        _context.Credentials.Update(credential);

        return Task.CompletedTask;
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}