using Microsoft.Extensions.Logging;

namespace ExamApp.Infrastructure.Jobs
{
    public class AutoSubmitExamJob : IAutoSubmitExamJob
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<AutoSubmitExamJob> _logger;

        public AutoSubmitExamJob(ApplicationDbContext context, ILogger<AutoSubmitExamJob> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task ExecuteAsync(string studentExamId, CancellationToken cancellationToken = default)
        {
            try
            {
                _logger.LogInformation("Starting auto-submit for StudentExam {StudentExamId}", studentExamId);

                var studentExam = await _context.StudentExams
                    .Include(se => se.Exam)
                    .Include(se => se.StudentAnswers)
                    .Include(se => se.StudentAnswers)
                        .ThenInclude(sa => sa.Question)
                    .FirstOrDefaultAsync(se => se.Id == studentExamId, cancellationToken);

                if (studentExam == null)
                {
                    _logger.LogWarning("StudentExam {StudentExamId} not found for auto-submit", studentExamId);
                    return;
                }

                // Check if already completed
                if (studentExam.CompletedAt.HasValue)
                {
                    _logger.LogInformation("StudentExam {StudentExamId} already completed at {CompletedAt}",
                        studentExamId, studentExam.CompletedAt);
                    return;
                }

                // Check if started
                if (!studentExam.StartedAt.HasValue)
                {
                    _logger.LogWarning("StudentExam {StudentExamId} was never started", studentExamId);
                    return;
                }

                // Calculate duration
                var elapsedMinutes = (DateTime.UtcNow - studentExam.StartedAt.Value).TotalMinutes;
                var allowedDuration = studentExam.Exam.DurationInMinutes;

                // Only auto-submit if duration has passed
                if (elapsedMinutes >= allowedDuration)
                {
                    studentExam.CompletedAt = studentExam.StartedAt.Value.AddMinutes(allowedDuration);

                    // Calculate score
                    studentExam.Score = studentExam.StudentAnswers
                        .Count(sa => sa.SelectedOptionIndex == sa.Question.CorrectOptionIndex);

                    await _context.SaveChangesAsync(cancellationToken);

                    _logger.LogInformation(
                        "Auto-submitted StudentExam {StudentExamId} for Student {StudentId} with Score {Score}",
                        studentExamId, studentExam.StudentId, studentExam.Score);
                }
                else
                {
                    _logger.LogInformation(
                        "StudentExam {StudentExamId} not yet due for auto-submit. Elapsed: {Elapsed}min, Required: {Required}min",
                        studentExamId, elapsedMinutes, allowedDuration);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during auto-submit for StudentExam {StudentExamId}", studentExamId);
                throw;
            }
        }
    }
}