using task_mananger_api.Domain.Entities;

namespace task_mananger_api.Domain.Interfaces;

public interface ITaskRepository
{
    Task<List<TaskEntity>> GetAllAsync();

    TaskEntity? GetById(int id);

    TaskEntity Create(TaskEntity task);

    void Update(TaskEntity task);

    void Delete(TaskEntity task);
}