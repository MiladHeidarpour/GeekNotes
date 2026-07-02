namespace GeekNotes.Modules.Users.Infrastructure.Persistence;

public class UserRepository(UserContext context) : IUserRepository
{
    public Task<bool> ExistsByIdAsync(UserId userId, CancellationToken cancellationToken)
        => context.Users.AnyAsync(x => x.Id == userId, cancellationToken);


    public Task<bool> ExistsByEmailAsync(Email email, CancellationToken cancellationToken)
        => context.Users.AnyAsync(x => x.Email == email, cancellationToken);


    public Task<bool> ExistsByPhoneNumberAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken)
        => context.Users.AnyAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);


    public Task<bool> ExistsByUserNameAsync(UserName userName, CancellationToken cancellationToken)
        => context.Users.AnyAsync(x => x.UserName == userName, cancellationToken);


    public Task<User?> GetByIdAsync(UserId userId, CancellationToken cancellationToken)
        => context.Users.FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);


    public Task<User?> GetByEmailAsync(Email email, CancellationToken cancellationToken)
        => context.Users.FirstOrDefaultAsync(x => x.Email == email, cancellationToken);


    public Task<User?> GetByPhoneNumberAsync(PhoneNumber phoneNumber, CancellationToken cancellationToken)
        => context.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber, cancellationToken);


    public Task<User?> GetByUserNameAsync(UserName userName, CancellationToken cancellationToken)
        => context.Users.FirstOrDefaultAsync(x => x.UserName == userName, cancellationToken);


    public async Task<IReadOnlyCollection<User>> GetUsersAsync(int pageNumber,
                                                               int pageSize,
                                                               string? fullName,
                                                               CancellationToken cancellationToken)
    {
        var users = await context.Users.AsNoTracking()
                                             .OrderByDescending(x => x.JoinedOnUtc)
                                             .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                                             .ToListAsync(cancellationToken);
        return [.. users];
    }


    public async Task<int> CountUsersAsync(CancellationToken cancellationToken)
    {
        var users = await context.Users.AsNoTracking()
                                       .ToListAsync(cancellationToken);
        return users.Count;
    }


    public void Add(User user)
        => context.Users.Add(user);


    public void Delete(User user)
        => context.Users.Remove(user);


    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }


    public Task<User?> GetByGithubIdAsync(
    string githubId,
    CancellationToken cancellationToken)
    {
        return context.Users
            .FirstOrDefaultAsync(
                x => x.GithubId == githubId,
                cancellationToken);
    }
}
