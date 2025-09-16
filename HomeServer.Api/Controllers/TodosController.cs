using Microsoft.AspNetCore.Mvc;
using HomeServer.Core;

namespace HomeServer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodosController : ControllerBase
    {
        private readonly Todo todo;

        public TodosController(Todo todo)
        {
            this.todo = todo;
        }

        [HttpGet("Notes")]
        public ActionResult<IReadOnlyCollection<string>> GetNotes()
        {
            return Ok(todo.GetNotes());
        }

        [HttpGet("SubTasks")]
        public ActionResult<IReadOnlyCollection<string>> GetSubTasks()
        {
            return Ok(todo.GetSubTasks());
        }

        [HttpPost("AddNote")]
        public IActionResult AddNote([FromBody] string note)
        {
            todo.AddNote(note);
            return NoContent();
        }

        [HttpPost("AddSubTask")]
        public IActionResult AddSubTask([FromBody] string subTask)
        {
            todo.AddSubTask(subTask);
            return NoContent();
        }

        [HttpDelete("RemoveSubTask")]
        public IActionResult RemoveSubTask([FromBody] string subTask)
        {
            todo.RemoveSubTask(subTask);
            return NoContent();
        }

        [HttpPost("CompleteSubTask")]
        public IActionResult CompleteSubTask([FromBody] string subTask)
        {
            var allCompleted = todo.CompleteSubTask(subTask);
            return Ok(allCompleted);

        }

        [HttpPost("UnmarkSubtaskAsCompleted")]
        public IActionResult UnmarkSubtaskAsCompleted([FromBody] string subTask)
        {
            todo.UnmarkSubtaskAsCompleted(subTask);
            return NoContent();
        }

        [HttpGet("StartedAt")]
        public IActionResult GetStartedAt()
        {
            return Ok(todo.StartedAt);
        }

        [HttpGet("IsCompleted")]
        public IActionResult GetIsCompleted()
        {
            return Ok(todo.IsCompleted);
        }

        [HttpGet("IsRecurring")]
        public IActionResult GetIsRecurring()
        {
            return Ok(todo.IsRecurring);
        }

        [HttpGet("Priority")]
        public IActionResult GetPriority()
        {
            return Ok(todo.Priority);
        }

        [HttpGet("Title")]
        public IActionResult GetTitle()
        {
            return Ok(todo.Title);
        }

        [HttpGet("Description")]
        public IActionResult GetDescription()
        {
            return Ok(todo.Description);
        }

        [HttpGet("DueDate")]
        public IActionResult GetDueDate()
        {
            return Ok(todo.DueDate);
        }

        [HttpGet("CreatedAt")]
        public IActionResult GetCreatedAt()
        {
            return Ok(todo.CreatedAt);
        }

        [HttpGet("UpdatedAt")]
        public IActionResult GetUpdatedAt()
        {
            return Ok(todo.UpdatedAt);
        }

        [HttpGet("CompletedAt")]
        public IActionResult GetCompletedAt()
        {
            return Ok(todo.CompletedAt);
        }

        [HttpPost("SetTitle")]
        public IActionResult SetTitle([FromBody] string title)
        {
            todo.Title = title;
            return NoContent();
        }

        [HttpPost("SetDescription")]
        public IActionResult SetDescription([FromBody] string description)
        {
            todo.Description = description;
            return NoContent();
        }

        [HttpPost("SetPriority")]
        public IActionResult SetPriority([FromBody] PriorityLevel priority)
        {
            todo.Priority = priority;
            return NoContent();
        }

        [HttpPost("SetDueDate")]
        public IActionResult SetDueDate([FromBody] DateTime? dueDate)
        {
            todo.DueDate = dueDate;
            return NoContent();
        }

        [HttpPost("SetIsRecurring")]
        public IActionResult SetIsRecurring([FromBody] bool isRecurring)
        {
            todo.IsRecurring = isRecurring;
            return NoContent();
        }

        [HttpPost("SetIsCompleted")]
        public IActionResult SetIsCompleted([FromBody] bool isCompleted)
        {
            todo.IsCompleted = isCompleted;
            return NoContent();
        }
    }
}
