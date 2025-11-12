
namespace ExamApp.Application.Features.Exam.Commands.DeleteExam
{
    internal class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;
        public DeleteExamCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(DeleteExamCommand request, CancellationToken cancellationToken)
        {
            var exam = await _unitOfWork.Exams.GetByIdAsync(request.id);
            if (exam is null)
            {
                return Result.Failure("Exam not found.");
            }
            _unitOfWork.Exams.Delete(exam);
            await _unitOfWork.SaveAsync();
            return Result.Success("Exam deleted successfully.");
        }
    }
}
