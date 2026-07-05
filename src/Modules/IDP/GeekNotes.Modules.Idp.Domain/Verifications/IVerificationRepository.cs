namespace GeekNotes.Modules.Idp.Domain.Verifications;

public interface IVerificationRepository
{
    Task AddAsync(Verification verification, CancellationToken cancellationToken = default);
    Task<Verification?> GetByIdAsync(VerificationId id, CancellationToken cancellationToken = default);
    Task<Verification?> GetAsync(Guid userId, VerificationType type, VerificationCode code, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Verification>> GetActiveAsync(Guid userId, VerificationType type, CancellationToken cancellationToken = default);
    void Update(Verification verification);
    void Remove(Verification verification);
}