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
using GeekNotes.Modules.Users.Presentation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GeekNotes.Modules.Identity.Presentation.Controllers;

public class RoleController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    public RoleController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    #region Commands
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleRequest request, CancellationToken cancellationToken)
    {

        var command = _mapper.Map<CreateRoleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleResult(response);
    }


    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateRoleRequest request, CancellationToken cancellationToken)
    {

        var command = _mapper.Map<UpdateRoleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleResult(response);
    }


    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteRoleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<DeleteRoleCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleResult(response);
    }
    #endregion


    #region Queries
    [HttpGet("GetRoles")]
    public async Task<IActionResult> GetRoles([FromQuery] GetRolesRequest request, CancellationToken cancellationToken = default)
    {
        var command = _mapper.Map<GetRolesQuery>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<PaginatedList<GetRolesResponse>>);
    }


    [HttpGet("{RoleId}")]
    public async Task<IActionResult> GetRole([FromRoute] GetRoleRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<GetRoleQuery>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<GetRoleResponse>);
    }
    #endregion
}
