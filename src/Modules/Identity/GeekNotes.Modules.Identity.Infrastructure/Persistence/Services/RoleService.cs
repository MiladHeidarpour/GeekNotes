using BuildingBlocks.Identity.Abstractions;

namespace GeekNotes.Modules.Identity.Infrastructure.Persistence.Services;

internal class RoleService(IRoleRepository repository) : IRoleService
{
    private readonly IRoleRepository _repository= repository;
    public Task<bool> ExistsAsync(Guid roleId, CancellationToken cancellationToken)
    {
        return _repository.ExistsByIdAsync(RoleId.Create(roleId), cancellationToken);
    }
}
