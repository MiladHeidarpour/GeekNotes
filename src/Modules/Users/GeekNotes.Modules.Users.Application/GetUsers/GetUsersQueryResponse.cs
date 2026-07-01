namespace GeekNotes.Modules.Users.Application.GetUsers;

public sealed record GetUsersQueryResponse(
    UserId UserId,
    UserName UserName,
    string FullName,
    Email Email,
    PhoneNumber PhoneNumber,
    string Avatar,
    DateTime JoinedOnUtc,
    IReadOnlyCollection<UserRole> Roles)
{
    public static explicit operator GetUsersQueryResponse(User user)
        => new GetUsersQueryResponse(user.Id,
                                    user.UserName,
                                    user.FullName,
                                    user.Email,
                                    user.PhoneNumber,
                                    user.Avatar,
                                    user.JoinedOnUtc,
                                    user.Roles);
}
