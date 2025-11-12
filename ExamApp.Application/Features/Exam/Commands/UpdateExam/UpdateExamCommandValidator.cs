namespace ExamApp.Application.Features.Exam.Commands.UpdateExam
{
    public class UpdateExamCommandValidator : AbstractValidator<UpdateExamCommand>
    {
        public UpdateExamCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Exam ID is required.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Exam name is required.")
                .MaximumLength(100).WithMessage("Exam name cannot exceed 100 characters.");

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate)
                .WithMessage("Start date must be before end date.");

            RuleFor(x => x.EndDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("End date must be in the future.");

            RuleFor(x => x.DurationInMinutes)
                .GreaterThan(0)
                .WithMessage("Duration must be greater than zero.");

            RuleFor(x => x.TotalMarks)
                .GreaterThan(0)
                .WithMessage("Total marks must be greater than zero.");
        }

    }

}
