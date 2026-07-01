namespace GeekNotes.Modules.Users.Application.Delete;

public sealed class DeleteUserCommandHandler(IUserRepository repository)
    : IRequestHandler<DeleteUserCommand, OperationResult>
{
    public async Task<OperationResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await repository.GetByIdAsync(request.UserId, cancellationToken);

        if (user is null)
            return OperationResult.NotFound();

        repository.Delete(user);

        await repository.SaveChangesAsync(cancellationToken);
        return OperationResult.Success();
    }
}