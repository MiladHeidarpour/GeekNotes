using GeekNotes.BuildingBlocks.Domain;
using System.Text.RegularExpressions;

namespace GeekNotes.Modules.Users.Domain.Exceptions;

public sealed class InvalidPhoneNumberException
    : DomainException
{
    private const string Message = "Invalid Phone Number.";

    public InvalidPhoneNumberException()
        : base(Message)
    {
    }

    public static void Throw(string phoneNumber)
    {
        if (string.IsNullOrWhiteSpace(phoneNumber))
            throw new InvalidPhoneNumberException();

        if (!Regex.IsMatch(phoneNumber, @"^09\d{9}$"))
            throw new InvalidPhoneNumberException();
    }
}
