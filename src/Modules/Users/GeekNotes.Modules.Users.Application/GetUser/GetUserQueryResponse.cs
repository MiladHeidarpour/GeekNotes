namespace GeekNotes.Modules.Users.Application.GetUser;

public sealed record GetUserQueryResponse(
    UserId UserId,
    UserName UserName,
    string FullName,
    Email Email,
    PhoneNumber PhoneNumber,
    string Avatar,
    DateTime JoinedOnUtc,
    IReadOnlyCollection<UserRole> Roles)
{
    public static explicit operator GetUserQueryResponse(User user)
        => new GetUserQueryResponse(user.Id,
                                    user.UserName,
                                    user.FullName,
                                    user.Email,
                                    user.PhoneNumber,
                                    user.Avatar,
                                    user.JoinedOnUtc,
                                    user.Roles);
}
