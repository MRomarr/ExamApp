using ExamApp.Application.Features.Exam.Commands.CreateExam;
using ExamApp.Application.Features.Exam.Commands.UpdateExam;
namespace ExamApp.Application.Features.Exam
{
    internal class ExamMappingProfile : Profile
    {
        public ExamMappingProfile()
        {
            CreateMap<CreateExamCommand, Domain.Entites.Exam>();

            CreateMap<UpdateExamCommand, Domain.Entites.Exam>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Questions, opt => opt.Ignore());

            CreateMap<Domain.Entites.Exam, ExamDto>();

        }
    }
}
