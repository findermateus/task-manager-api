using Microsoft.AspNetCore.Mvc;
using task_mananger_api.Domain.UseCases;
using task_mananger_api.DTOs;

namespace task_mananger_api.Controllers;

[ApiController]
[Route("tasks")]
public class TasksController(
    GetAllTasks getAllTasks,
    CreateTask createTask,
    StartTask startTask,
    GetTaskById getTaskById,
    CancelTask cancelTask,
    CompleteTask completeTask,
    DeleteTask deleteTask,
    UpdateTask updateTask) : ControllerBase
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

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var taskEntity = getTaskById.Execute(id);

        if (taskEntity is null)
        {
            return NotFound();
        }

        return Ok(taskEntity);
    }

    [HttpPatch("{id}/start")]
    public IActionResult Start(int id)
    {
        var result = startTask.Execute(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("{id}/cancel")]
    public IActionResult Cancel(int id)
    {
        var result = cancelTask.Execute(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpPatch("{id}/complete")]
    public IActionResult Complete(int id)
    {
        var result = completeTask.Execute(id);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var result = deleteTask.Execute(id);

        if (result is null)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateTaskDto payload)
    {
        var result = updateTask.Execute(id, payload);

        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }
}