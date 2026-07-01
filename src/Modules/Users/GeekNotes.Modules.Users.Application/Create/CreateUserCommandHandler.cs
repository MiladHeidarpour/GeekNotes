using BuildingBlocks.Identity.Abstractions;

namespace GeekNotes.Modules.Users.Application.Create;

internal sealed class CreateUserCommandHandler(IUserRepository repository, IRoleService roleService)
    : IRequestHandler<CreateUserCommand, OperationResult<CreateUserCommandResponse>>
{
    public async Task<OperationResult<CreateUserCommandResponse>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var email = Email.Create(request.Email);
        var phone = PhoneNumber.Create(request.PhoneNumber);
        var userName = UserName.Create(request.UserName);

        if (await repository.ExistsByEmailAsync(email, cancellationToken))
        {
            return OperationResult<CreateUserCommandResponse>.Error("Email already exists.");
        }

        if (await repository.ExistsByPhoneNumberAsync(phone, cancellationToken))
        {
            return OperationResult<CreateUserCommandResponse>.Error("Phone number already exists.");
        }

        if (await repository.ExistsByUserNameAsync(userName, cancellationToken))
        {
            return OperationResult<CreateUserCommandResponse>.Error("Username already exists.");
        }

        var roleIds = new List<Guid>();

        foreach (var roleId in request.RoleIds)
        {
            if (!await roleService.ExistsAsync(roleId, cancellationToken))
            {
                return OperationResult<CreateUserCommandResponse>.Error($"Role '{roleId}' not found.");
            }

            roleIds.Add(roleId);
        }

        var user = User.Create(
            email,
            phone,
            userName,
            request.FullName,
            request.PasswordHash,
            roleIds);

        repository.Add(user);
        await repository.SaveChangesAsync(cancellationToken);

        return OperationResult<CreateUserCommandResponse>.Success(new CreateUserCommandResponse(user.Id));
    }
}