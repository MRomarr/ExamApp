using ExamApp.Application.Features.Auth.Commands.RegisterUser;

namespace ExamApp.Application.Features.Auth
{
    internal class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            CreateMap<RegisterUserCommand, ApplicationUser>();
        }
    }
}
