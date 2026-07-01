namespace BuildingBlocks.Identity.Abstractions;

public interface IRoleService
{
    Task<bool> ExistsAsync(Guid roleId, CancellationToken cancellationToken);
}
