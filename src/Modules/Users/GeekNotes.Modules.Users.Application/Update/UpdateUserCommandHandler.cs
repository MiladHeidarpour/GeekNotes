namespace GeekNotes.Modules.Users.Application.Update;

public sealed class UpdateUserCommandHandler(IUserRepository repository)
    : IRequestHandler<UpdateUserCommand, OperationResult>
{
    public async Task<OperationResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return OperationResult.NotFound();

        user.Update(request.FullName, Email.Create(request.Email), PhoneNumber.Create(request.PhoneNumber));

        await repository.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}