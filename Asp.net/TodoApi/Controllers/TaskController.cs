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
        public async Task<ActionResult<Todo>> GetTodoById(string id)
        {
            try
            {
                return Ok(await service.GetTodoById(id));
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        
    }
}
