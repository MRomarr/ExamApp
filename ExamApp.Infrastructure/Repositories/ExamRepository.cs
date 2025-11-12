namespace ExamApp.Infrastructure.Repositories
{
    public class ExamRepository : GenericRepository<Exam>, IExamRepository
    {
        private readonly ApplicationDbContext _context;

        public ExamRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Exam> GetExamWithDetailsAsync(string id)
        {
            return await _context.Exams
                .Include(e => e.Questions)
                .ThenInclude(q => q.QuestionOptions)
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }

}
