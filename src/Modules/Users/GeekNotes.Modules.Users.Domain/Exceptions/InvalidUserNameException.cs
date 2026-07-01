using GeekNotes.BuildingBlocks.Domain;
using System.Text.RegularExpressions;

namespace GeekNotes.Modules.Users.Domain.Exceptions;

public sealed class InvalidUserNameException : DomainException
{
    private const string Message = "Invalid User Name.";

    public InvalidUserNameException()
        : base(Message)
    {
    }

    public static void Throw(string userName)
    {
        if (string.IsNullOrWhiteSpace(userName))
            throw new InvalidUserNameException();

        if (userName.Length < 3)
            throw new InvalidUserNameException();

        if (userName.Length > 50)
            throw new InvalidUserNameException();

        if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9._]{3,50}$"))
            throw new InvalidUserNameException();
    }
}