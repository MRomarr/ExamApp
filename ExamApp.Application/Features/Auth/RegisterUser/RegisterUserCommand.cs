namespace ExamApp.Application.Features.Auth.RegisterUser
{
    public class RegisterUserCommand : IRequest<Result<AuthDto>>
    {
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
