namespace ExamApp.Application.Features.Auth.Commands.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<AuthDto>>;
}
