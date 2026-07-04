using GeekNotes.BuildingBlocks.Application;
using GeekNotes.Modules.Users.Application.Create;
using GeekNotes.Modules.Users.Application.Delete;
using GeekNotes.Modules.Users.Application.GetUser;
using GeekNotes.Modules.Users.Application.GetUsers;
using GeekNotes.Modules.Users.Application.Update;
using GeekNotes.Modules.Users.Contracts.Requests.Create;
using GeekNotes.Modules.Users.Contracts.Requests.Delete;
using GeekNotes.Modules.Users.Contracts.Requests.GetUser;
using GeekNotes.Modules.Users.Contracts.Requests.GetUsers;
using GeekNotes.Modules.Users.Contracts.Requests.Update;
using GeekNotes.Modules.Users.Contracts.Responses.Create;
using GeekNotes.Modules.Users.Contracts.Responses.GetUser;
using GeekNotes.Modules.Users.Contracts.Responses.GetUsers;
using GeekNotes.Modules.Users.Domain;
using Mapster;

namespace GeekNotes.Modules.Users.Presentation.Mappers;

public sealed class UserMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<Guid, UserId>()
            .MapWith(src => UserId.Create(src));

        config.NewConfig<Email, string>()
      .MapWith(src => src.Value);

        config.NewConfig<PhoneNumber, string>()
              .MapWith(src => src.Value);

        config.NewConfig<UserName, string>()
              .MapWith(src => src.Value);

        config.NewConfig<UserRole, Guid>()
      .MapWith(src => src.RoleId);

        config.ForType<CreateUserRequest, CreateUserCommand>();
        config.ForType<CreateUserCommandResponse, CreateUserResponse>()
            .Map(x => x.UserId, src => src.UserId.Value);


        config.NewConfig<UpdateUserRequest, UpdateUserCommand>();


        config.NewConfig<DeleteUserRequest, DeleteUserCommand>();



        config.NewConfig<GetUserRequest, GetUserQuery>();

        config.ForType<GetUserQueryResponse, GetUserResponse>()
            .Map(x => x.UserId, src => src.UserId.ToString())
            .Map(x => x.Email, src => src.Email)
            .Map(x => x.PhoneNumber, src => src.PhoneNumber)
            .Map(x => x.UserName, src => src.UserName)
            .Map(x => x.FullName, src => src.FullName)
            .Map(x => x.Avatar, src => src.Avatar)
            .Map(x => x.JoinedOnUtc, src => src.JoinedOnUtc)
            .Map(x => x.Roles, src => src.Roles);


        config.ForType<GetUsersRequest, GetUsersQuery>()
                   .Map(x => x.PageNumber, src => src.Page)
                   .Map(x => x.PageSize, src => src.Size)
                   .Map(x => x.FullName, src => src.FullName);

        config.ForType<GetUsersQueryResponse, GetUsersResponse>()
            .Map(x => x.UserId, src => src.UserId.ToString())
            .Map(x => x.Email, src => src.Email)
            .Map(x => x.PhoneNumber, src => src.PhoneNumber)
            .Map(x => x.UserName, src => src.UserName)
            .Map(x => x.FullName, src => src.FullName)
            .Map(x => x.Avatar, src => src.Avatar)
            .Map(x => x.JoinedOnUtc, src => src.JoinedOnUtc);



        config.ForType<PaginatedList<GetUsersQueryResponse>, PaginatedList<GetUsersResponse>>()
            .ConstructUsing(src =>
                new PaginatedList<GetUsersResponse>
                {
                    Items = src.Items
                        .Select(x => x.Adapt<GetUsersResponse>())
                        .ToList(),

                    PageNumber = src.PageNumber,
                    TotalPages = src.TotalPages,
                    TotalCount = src.TotalCount
                });
    }
}
