namespace GeekNotes.Modules.Users.Domain;

public interface IUserRepository
{
    Task<bool> ExistsByIdAsync(UserId userId, CancellationToken cancellationToken);
    Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken);
    Task<bool> ExistsByPhoneNumberAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken);
    Task<bool> ExistsByUserNameAsync(UserName userName, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken);
    Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken);
    Task<User?> GetByPhoneNumberAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken);
    Task<User?> GetByUserNameAsync(UserName userName, CancellationToken cancellationToken);
    Task<IReadOnlyCollection<User>> GetUsersAsync(int pageNumber, int pageSize, string? fullName, CancellationToken cancellationToken);
    Task<int> CountUsersAsync(CancellationToken cancellationToken);
    void Add(User user);
    void Delete(User user);
    Task SaveChangesAsync(CancellationToken cancellationToken);

    Task<User?> GetByGithubIdAsync(
        string githubId,
        CancellationToken cancellationToken);
}
