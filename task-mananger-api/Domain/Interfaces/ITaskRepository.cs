using task_mananger_api.Domain.Entities;

namespace task_mananger_api.Domain.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetAllAsync();
}