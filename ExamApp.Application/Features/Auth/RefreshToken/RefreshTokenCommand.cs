namespace ExamApp.Application.Features.Auth.RefreshToken
{
    public record RefreshTokenCommand(string RefreshToken) : IRequest<Result<AuthDto>>;
}
