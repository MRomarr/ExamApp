namespace ExamApp.Application.Features.Question.Command.AddQuestion
{
    public class AddQuestionCommandHandlerValidator : AbstractValidator<AddQuestionCommand>
    {
        public AddQuestionCommandHandlerValidator()
        {
            RuleFor(x => x.Text)
                .NotNull().WithMessage("AddQuestionCommandHandler cannot be null.");
            RuleFor(x => x.ExamId)
                .NotNull().WithMessage("ExamId cannot be null.");
            RuleFor(x => x.Marks)
                .GreaterThan(0).WithMessage("Marks must be greater than zero.");
            RuleFor(x => x.Options)
                .NotNull().WithMessage("Options cannot be null.")
                .Must(options => options != null && options.Count >= 2)
                .WithMessage("There must be at least two options.");
            RuleFor(x => x.CorrectOptionIndex)
                .Must((command, correctOptionIndex) =>
                    command.Options != null &&
                    correctOptionIndex >= 0 &&
                    correctOptionIndex < command.Options.Count)
                .WithMessage("CorrectOptionIndex must be a valid index in the Options list.");

        }
    }

}
