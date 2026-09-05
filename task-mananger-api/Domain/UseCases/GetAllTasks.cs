using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Interfaces;

namespace task_mananger_api.Domain.UseCases;

public class GetAllTasks(ITaskRepository taskRepository)
{
    public async Task<List<TaskEntity>> Execute()
    {
        return await taskRepository.GetAllAsync();
    }
}