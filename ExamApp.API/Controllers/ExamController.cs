using ExamApp.Application.Features.Exam.Commands.AssignExamToUser;
using ExamApp.Application.Features.Exam.Commands.CreateExam;
using ExamApp.Application.Features.Exam.Commands.DeleteExam;
using ExamApp.Application.Features.Exam.Commands.StartExam;
using ExamApp.Application.Features.Exam.Commands.SubmitExam;
using ExamApp.Application.Features.Exam.Commands.UpdateExam;
using ExamApp.Application.Features.Exam.Queries.GetAllExams;
using ExamApp.Application.Features.Exam.Queries.GetExamById;
using ExamApp.Application.Features.Exam.Queries.GetExamByIdWithDetails;
using ExamApp.Application.Features.Exam.Queries.GetExamResult;
using SharedKernel;

namespace ExamApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExamController(IMediator mediator) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await mediator.Send(new GetAllExamsQuery());
            return Ok(result);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await mediator.Send(new GetExamByIdQuery(id));
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }


        [HttpGet("{id}/detalis")]
        [Authorize]
        public async Task<IActionResult> GetByIdWithDetails(string id)
        {
            var result = await mediator.Send(new GetExamByIdWithDetailsQuery(id));
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }


        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> CreateExam(CreateExamCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }


        [HttpPut]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> UpdateExam(UpdateExamCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> DeleteExam(string id)
        {
            var result = await mediator.Send(new DeleteExamCommand(id));
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Message);
        }


        [HttpPost("assign/{examId}/to/{userId}")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> AssignExamToUser(AssignExamCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Message);
        }


        [HttpPost("start/{examId}")]
        [Authorize]
        public async Task<IActionResult> StartExam(StartExamCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }


        [HttpPost("submit/{examId}")]
        [Authorize]
        public async Task<IActionResult> SubmitExam(SubmitExamCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Message);
        }


        [HttpGet("Result/{examId}")]
        [Authorize]
        public async Task<IActionResult> SubmitExam(GetExamResultQuery command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Data);
        }
    }
}
