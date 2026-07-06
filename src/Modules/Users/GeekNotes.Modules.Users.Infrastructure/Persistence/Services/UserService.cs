using BuildingBlocks.User.Abstractions;

namespace GeekNotes.Modules.Users.Infrastructure.Persistence.Services;

internal class UserService(IUserRepository repository) : IUserService
{
    private readonly IUserRepository _repository = repository;
    public async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await _repository.ExistsByIdAsync(UserId.Create(userId), cancellationToken);
    }

    public async Task<UserDto?> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByEmailAsync(Email.Create(email), cancellationToken);
        if (user is null)
        {
            return null;
        }

        return new UserDto(Guid.Parse(user.Id.ToString()),
                           user.Email.ToString(),
                           user.PhoneNumber.ToString(),
                           user.UserName.ToString(),
                           user.FullName,
                           user.Avatar,
                           user.JoinedOnUtc,
                           (IReadOnlyCollection<string>)user.Roles);
    }

    public Task<UserDto?> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
