using Microsoft.AspNetCore.Mvc;
using task_mananger_api.Domain.UseCases;

namespace task_mananger_api.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController(GetAllTasks getAllTasks) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await getAllTasks.Execute();

        return Ok(tasks);
    }
}