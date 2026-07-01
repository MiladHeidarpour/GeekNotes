using GeekNotes.BuildingBlocks.Domain;
using System.Net.Mail;

namespace GeekNotes.Modules.Users.Domain.Exceptions;

public class InvalidEmailAddressException : DomainException
{
    private const string _messages = "Invalid Email Address.";

    public InvalidEmailAddressException() : base(_messages) { }

    public static void Throw(string email)
    {
        if (!MailAddress.TryCreate(email, out _))
        {
            throw new InvalidEmailAddressException();
        }
    }
}
