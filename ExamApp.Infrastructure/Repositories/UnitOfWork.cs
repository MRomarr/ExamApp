namespace ExamApp.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IRepository<Exam> _exams;
        private IRepository<StudentExam> _StudentExams;
        private IRepository<Question> _questions;
        private IRepository<StudentAnswer> _studentAnswers;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }
        //----
        public IRepository<Exam> Exams => _exams ??= new GenericRepository<Exam>(_context);
        public IRepository<StudentExam> StudentExams => _StudentExams ??= new GenericRepository<StudentExam>(_context);
        public IRepository<Question> Questions => _questions ??= new GenericRepository<Question>(_context);
        public IRepository<StudentAnswer> StudentAnswers => _studentAnswers ??= new GenericRepository<StudentAnswer>(_context);

        //----
        public async Task<bool> SaveAsync()
        {
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
