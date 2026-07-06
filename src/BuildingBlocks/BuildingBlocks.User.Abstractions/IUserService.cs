namespace BuildingBlocks.User.Abstractions;

public interface IUserService
{
    Task<UserDto?> GetByIdAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<UserDto?> GetByEmailAsync(
        string email,
        CancellationToken cancellationToken);

    Task<bool> ExistsAsync(
        Guid userId,
        CancellationToken cancellationToken);
}
public sealed record UserDto(
    Guid Id,
    string Email,
    string PhoneNumber,
    string UserName,
    string FullName,
    string Avatar,
    DateTime JoinedOnUtc,
    IReadOnlyCollection<string> Roles);