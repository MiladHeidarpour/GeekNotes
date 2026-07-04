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
using GeekNotes.Modules.Users.Contracts.Responses.GetUser;
using GeekNotes.Modules.Users.Contracts.Responses.GetUsers;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeekNotes.Modules.Users.Presentation.Controllers;

public class UserController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public UserController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    #region Commands
    //[HasPermission(ApiPermissions.ManageUsers)]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<CreateUserCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleResult(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateUserRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<UpdateUserCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleResult(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody] DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<DeleteUserCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleResult(response);
    }
    #endregion


    #region Queries

    //[HasPermission(ApiPermissions.ManageUsers)]
    [HttpGet("GetUsers")]
    public async Task<IActionResult> GetUsers([FromQuery] GetUsersRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetUsersQuery>(request);
        var response = await _mediator.Send(query, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<PaginatedList<GetUsersResponse>>);
    }

    [HttpGet("{UserId:guid}")]
    public async Task<IActionResult> GetUser(
        [FromRoute] GetUserRequest request,
        CancellationToken cancellationToken)
    {
        var query = _mapper.Map<GetUserQuery>(request);
        var response = await _mediator.Send(query, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<GetUserResponse>);
    }
    #endregion
}

