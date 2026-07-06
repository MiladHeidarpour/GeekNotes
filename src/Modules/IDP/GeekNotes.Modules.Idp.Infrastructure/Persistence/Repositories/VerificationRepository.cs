using GeekNotes.Modules.Idp.Domain.Verifications;
using Microsoft.EntityFrameworkCore;

namespace GeekNotes.Modules.Idp.Infrastructure.Persistence.Repositories;

internal sealed class VerificationRepository
    : IVerificationRepository
{
    private readonly IdpDbContext _context;

    public VerificationRepository(
        IdpDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Verification verification,
        CancellationToken cancellationToken = default)
    {
        await _context.Verifications.AddAsync(
            verification,
            cancellationToken);
    }

    public async Task<Verification?> GetByIdAsync(
        VerificationId id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Verifications
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Verification?> GetAsync(
        Guid userId,
        VerificationType type,
        VerificationCode code,
        CancellationToken cancellationToken = default)
    {
        return await _context.Verifications
            .FirstOrDefaultAsync(
                x =>
                    x.UserId == userId &&
                    x.Type == type &&
                    x.Code == code,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Verification>> GetActiveAsync(
        Guid userId,
        VerificationType type,
        CancellationToken cancellationToken = default)
    {
        return await _context.Verifications
            .Where(x =>
                x.UserId == userId &&
                x.Type == type &&
                x.UsedOnUtc == null)
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(
    Verification verification,
    CancellationToken cancellationToken = default)
    {
        _context.Verifications.Update(verification);

        return Task.CompletedTask;
    }

    public Task RemoveAsync(
        Verification verification,
        CancellationToken cancellationToken = default)
    {
        _context.Verifications.Remove(verification);

        return Task.CompletedTask;
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
