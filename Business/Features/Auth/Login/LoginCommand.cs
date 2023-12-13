using MediatR;

namespace Business.Features.Auth.Login;

public sealed record LoginCommand(
    string UserNameOrEmail,
    string Password) : IRequest<LoginCommandResponse>;
