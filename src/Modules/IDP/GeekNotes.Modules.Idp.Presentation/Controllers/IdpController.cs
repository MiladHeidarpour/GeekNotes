using GeekNotes.BuildingBlocks.Application;
using GeekNotes.Modules.Idp.Application.Authentications.Login;
using GeekNotes.Modules.Idp.Application.Authentications.Logout;
using GeekNotes.Modules.Idp.Application.Authentications.Me;
using GeekNotes.Modules.Idp.Application.Authentications.RefreshToken;
using GeekNotes.Modules.Idp.Application.Authentications.Register;
using GeekNotes.Modules.Idp.Application.Authentications.Sessions.RevokeAllSessions;
using GeekNotes.Modules.Idp.Application.Authentications.Sessions.RevokeSession;
using GeekNotes.Modules.Idp.Contracts.Login;
using GeekNotes.Modules.Idp.Contracts.Logout;
using GeekNotes.Modules.Idp.Contracts.Me;
using GeekNotes.Modules.Idp.Contracts.RefreshToken;
using GeekNotes.Modules.Idp.Contracts.Register;
using GeekNotes.Modules.Idp.Contracts.RevokeAllSessions;
using GeekNotes.Modules.Idp.Contracts.RevokeSession;
using GeekNotes.Modules.Users.Presentation;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GeekNotes.Modules.Idp.Presentation.Controllers;

public class IdpController : ApiController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;
    public IdpController(IMediator mediator, IMapper mapper, ICurrentUser currentUser)
    {
        _mediator = mediator;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RegisterCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<RegisterResponse>);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request, CancellationToken cancellationToken)
    {
        var command = _mapper.Map<LoginCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<LoginResponse>);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(
    RefreshTokenRequest request,
    CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RefreshTokenCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<RefreshTokenResponse>);
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
    LogoutRequest request,
    CancellationToken cancellationToken)
    {
        var command = _mapper.Map<LogoutCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<LogoutResponse>);
    }

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> Me(CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new MeQuery(), cancellationToken);
        return HandleMappedResult(response, _mapper.Map<MeResponse>);
    }

    [HttpDelete("sessions/{sessionId:guid}")]
    public async Task<IActionResult> RevokeSession(
    RevokeSessionRequest request,
    CancellationToken cancellationToken)
    {
        var command = _mapper.Map<RevokeSessionCommand>(request);
        var response = await _mediator.Send(command, cancellationToken);
        return HandleMappedResult(response, _mapper.Map<RevokeSessionResponse>);
    }

    [HttpDelete("sessions")]
    public async Task<IActionResult> RevokeAllSessions(
    CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new RevokeAllSessionsCommand(), cancellationToken);
        return HandleMappedResult(response, _mapper.Map<RevokeAllSessionsResponse>);
    }

    //[HttpGet("github/login")]
    //public IActionResult GithubLogin()
    //{
    //    var clientId = "Ov23liejpeWsMNWPl24F";
    //    var redirectUri = "https://localhost:7001/api/auth/github/callback";

    //    var url =
    //        "https://github.com/login/oauth/authorize" +
    //        $"?client_id={clientId}" +
    //        $"&redirect_uri={Uri.EscapeDataString(redirectUri)}" +
    //        $"&scope={Uri.EscapeDataString("read:user user:email")}";

    //    return Redirect(url);
    //}

    //[HttpGet("github/callback")]
    //public async Task<IActionResult> GithubCallback([FromQuery] string code, CancellationToken cancellationToken)
    //{
    //    var response = await _mediator.Send(new GithubLoginCommand(code), cancellationToken);
    //    return HandleMappedResult(response, _mapper.Map<GithubLoginCommandResponse>);

    //}
}