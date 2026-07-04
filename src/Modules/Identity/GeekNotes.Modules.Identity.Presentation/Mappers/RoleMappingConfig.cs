using GeekNotes.BuildingBlocks.Application;
using GeekNotes.Modules.Identity.Application.Roles.Create;
using GeekNotes.Modules.Identity.Application.Roles.Delete;
using GeekNotes.Modules.Identity.Application.Roles.GetRole;
using GeekNotes.Modules.Identity.Application.Roles.GetRoles;
using GeekNotes.Modules.Identity.Application.Roles.Update;
using GeekNotes.Modules.Identity.Contracts.Requests.Create;
using GeekNotes.Modules.Identity.Contracts.Requests.Delete;
using GeekNotes.Modules.Identity.Contracts.Requests.GetRole;
using GeekNotes.Modules.Identity.Contracts.Requests.GetRoles;
using GeekNotes.Modules.Identity.Contracts.Requests.Update;
using GeekNotes.Modules.Identity.Contracts.Responses.GetRole;
using GeekNotes.Modules.Identity.Contracts.Responses.GetRoles;
using GeekNotes.Modules.Identity.Domain.Roles;
using Mapster;

namespace GeekNotes.Modules.Identity.Presentation.Mappers;

public sealed class RoleMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<CreateRoleRequest, CreateRoleCommand>();



        config.ForType<DeleteRoleRequest, DeleteRoleCommand>()
                   .Map(x => x.RoleId, src => RoleId.Create(src.RoleId));



        config.ForType<UpdateRoleRequest, UpdateRoleCommand>()
                  .Map(x => x.RoleId, src => RoleId.Create(src.RoleId));



        config.ForType<GetRoleRequest, GetRoleQuery>()
                   .Map(x => x.RoleId, src => RoleId.Create(src.RoleId));
        config.ForType<GetRoleQueryResponse, GetRoleResponse>()
                    .Map(x => x.RoleId, src => src.RoleId.ToString())
                    .Map(x => x.Name, src => src.RoleName)
                    .Map(x => x.Permissions, src => src.Permissions);




        config.ForType<GetRolesRequest, GetRolesQuery>()
                   .Map(x => x.PageNumber, src => src.Page)
                   .Map(x => x.PageSize, src => src.Size)
                   .Map(x => x.Title, src => src.Title);
        config.ForType<GetRolesQueryResponse, GetRolesResponse>()
                    .Map(x => x.RoleId, src => src.RoleId.ToString())
                    .Map(x => x.Name, src => src.RoleName)
                    .Map(x => x.Permissions, src => src.Permissions);



        config.ForType<PaginatedList<GetRolesQueryResponse>, PaginatedList<GetRolesResponse>>()
            .ConstructUsing(src => new PaginatedList<GetRolesResponse>
            {
                Items = src.Items.Select(item => item.Adapt<GetRolesResponse>()).ToList(),
                PageNumber = src.PageNumber,
                TotalPages = src.TotalPages,
                TotalCount = src.TotalCount
            });
    }
}