namespace ExamApp.Application.Features.Question
{
    public class QuestionMappingProfile : Profile
    {
        public QuestionMappingProfile()
        {
            CreateMap<Command.AddQuestion.AddQuestionCommand, Domain.Entites.Question>();

        }
    }
}
