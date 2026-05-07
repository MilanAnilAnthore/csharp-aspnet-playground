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
        public async Task<IActionResult> Get()
        {
            return Ok(await service.GetAllTodosAsync());
        }

        // POST: api/Task
        [HttpPost]
        public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto request)
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
