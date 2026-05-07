using Microsoft.AspNetCore.Mvc;
using TodoApi.Repository;
using TodoApi.DTO;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TaskController(IServiceRepository service) : ControllerBase
    {
        // GET: api/todos
        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodo()
        {
            return Ok(await service.GetAllTodosAsync());
        }

        // GET: api/todos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodoById(Guid id)
        {
            try
            {
                var todo = await service.GetTodoById(id);
                return Ok(todo);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Task
        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] CreateTodoDto request)
        {
            try
            {

                await service.AddTodoAsync(request.Title, request.Priority, request.DueDate);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            try
            {
                await service.DeleteTodoAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

    }
}
