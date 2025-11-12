namespace ExamApp.Application.Features.Exam.Commands.CreateExam
{
    public class CreateExamCommandValidator : AbstractValidator<CreateExamCommand>
    {
        public CreateExamCommandValidator()
        {
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

            RuleFor(x => x)
                .Must(HaveMatchingTotalMarks)
                .WithMessage("The sum of question marks must equal the exam's total marks.");

            RuleForEach(x => x.Questions)
                .SetValidator(new CreateQuestionCommandValidator());
        }

        private bool HaveMatchingTotalMarks(CreateExamCommand exam)
        {
            if (exam.Questions == null || !exam.Questions.Any())
                return true; // Skip if no questions 

            var sumOfMarks = exam.Questions.Sum(q => q.Marks);
            return Math.Abs(sumOfMarks - exam.TotalMarks) < 0.0001; // Floating-point safe check
        }
    }

    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Question text is required.");

            RuleFor(x => x.Marks)
                .GreaterThan(0)
                .WithMessage("Each question must have marks greater than zero.");

            RuleFor(x => x.QuestionOptions)
                .NotEmpty().WithMessage("Each question must have at least one option.");

            RuleFor(x => x.QuestionOptions)
                .Must(HaveExactlyOneCorrectOption)
                .WithMessage("Each question must have exactly one correct option.");

            RuleForEach(x => x.QuestionOptions)
                .SetValidator(new CreateQuestionOptionCommandValidator());
        }

        private bool HaveExactlyOneCorrectOption(ICollection<CreateQuestionOptionCommand> options)
        {
            if (options == null || !options.Any())
                return false;

            int correctCount = options.Count(o => o.IsCorrect);
            return correctCount == 1;
        }
    }

    public class CreateQuestionOptionCommandValidator : AbstractValidator<CreateQuestionOptionCommand>
    {
        public CreateQuestionOptionCommandValidator()
        {
            RuleFor(x => x.Text)
                .NotEmpty().WithMessage("Option text is required.");
            RuleFor(x => x.IsCorrect)
                .NotNull().WithMessage("Option correctness must be specified.");
        }
    }
}
