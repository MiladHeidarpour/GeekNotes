using GeekNotes.Modules.Idp.Application.Authentications.Login;
using GeekNotes.Modules.Idp.Application.Authentications.Register;
using GeekNotes.Modules.Idp.Contracts.Requests;
using Mapster;

namespace GeekNotes.Modules.Idp.Presentation.Mappers;

public class IdpMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<RegisterRequest, RegisterCommand>();
        config.ForType<RegisterResponse, RegisterCommandResponse>();

        config.ForType<LoginRequest, LoginCommand>();
        config.ForType<LoginResponse, LoginCommandResponse>();
    }
}
