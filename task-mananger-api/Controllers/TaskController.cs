using Microsoft.AspNetCore.Mvc;
using task_mananger_api.Domain.UseCases;
using task_mananger_api.DTOs;

namespace task_mananger_api.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController(GetAllTasks getAllTasks, CreateTask createTask) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await getAllTasks.Execute();

        return Ok(tasks);
    }

    [HttpPost]
    public IActionResult Create(CreateTaskDto payload)
    {
        var task = createTask.Execute(payload);

        return Ok(task);
    }
}