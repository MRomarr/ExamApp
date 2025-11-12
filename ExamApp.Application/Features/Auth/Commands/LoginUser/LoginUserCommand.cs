namespace ExamApp.Application.Features.Auth.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<Result<AuthDto>>
    {
        public string UserNameOrEmail { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
