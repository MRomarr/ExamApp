using ExamApp.Application.Features.Question.Command.AddQuestion;
using ExamApp.Application.Features.Question.Command.DeleteQuestion;
using ExamApp.Application.Features.Question.Command.SubmitAnswer;
using SharedKernel;

namespace ExamApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController(Mediator mediator) : ControllerBase
    {
        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> AddQuestion(AddQuestionCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Message);
        }


        [HttpDelete]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> UpdateQuestion(DeleteQuestionCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Message);
        }

        [HttpPost("submitAnswer")]
        [Authorize]
        public async Task<IActionResult> SubmitAnswer(SubmitAnswerCommand command)
        {
            var result = await mediator.Send(command);
            if (!result.Succeeded)
                return BadRequest(result.Errors);
            return Ok(result.Message);
        }
    }
}
