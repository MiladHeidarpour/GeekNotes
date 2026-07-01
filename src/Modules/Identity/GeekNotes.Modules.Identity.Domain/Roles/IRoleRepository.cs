namespace GeekNotes.Modules.Identity.Domain.Roles;

public interface IRoleRepository
{
    Task<bool> ExistsByNameAsync(RoleName name, CancellationToken cancellationToken);
    Task<bool> ExistsByIdAsync(RoleId id, CancellationToken cancellationToken);
    void Add(Role role);
    Task<IReadOnlyCollection<Role>> GetRolesAsync(int pageNumber, int pageSize, string title, CancellationToken cancellationToken);
    Task<Role?> GetByIdAsync(RoleId roleId, CancellationToken cancellationToken);
    Task<Role?> GetByNameAsync(RoleName name, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
    void Delete(Role role);
    Task<int> CountRolesAsync(string? title, CancellationToken cancellationToken);
}
