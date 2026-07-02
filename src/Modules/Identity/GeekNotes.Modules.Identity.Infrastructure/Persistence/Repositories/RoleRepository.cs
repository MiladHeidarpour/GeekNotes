namespace GeekNotes.Modules.Identity.Infrastructure.Persistence.Repositories;

public class RoleRepository(IdentityContext context) : IRoleRepository
{
    public Task<bool> ExistsByNameAsync(RoleName name, CancellationToken cancellationToken)
        => context.Roles.AnyAsync(x => x.Name == name, cancellationToken);

    public Task<bool> ExistsByIdAsync(RoleId id, CancellationToken cancellationToken)
        => context.Roles.AnyAsync(x => x.Id == id, cancellationToken);


    public void Add(Role role)
        => context.Roles.Add(role);


    public async Task<IReadOnlyCollection<Role>> GetRolesAsync(int pageNumber,
                                                         int pageSize,
                                                         string title,
                                                         CancellationToken cancellationToken)
    {
        var roles = await context.Roles.AsNoTracking()
                                             .Where(x => x.Title.Contains(title) || x.Name.Value.Contains(title))
                                             .Skip((pageNumber - 1) * pageSize).Take(pageSize)
                                             .ToListAsync(cancellationToken);
        return [.. roles];
    }


    public Task<Role?> GetByIdAsync(RoleId roleId, CancellationToken cancellationToken)
        => context.Roles.FirstOrDefaultAsync(x => x.Id == roleId, cancellationToken);


    public Task<Role?> GetByNameAsync(RoleName name, CancellationToken cancellationToken)
        => context.Roles.FirstOrDefaultAsync(x => x.Name.Value == name.Value, cancellationToken);

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }


    public void Delete(Role role)
        => context.Roles.Remove(role);


    public async Task<int> CountRolesAsync(string? title, CancellationToken cancellationToken)
    {
        var roles = await context.Roles
                                        .AsNoTracking()
                                        .ToListAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(title))
        {
            return roles.Count(x => x.Title.Contains(title) || x.Name.Value.Contains(title));
        }

        return roles.Count;
    }
}
