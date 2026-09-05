using Microsoft.EntityFrameworkCore;
using task_mananger_api.Domain.Entities;
using task_mananger_api.Domain.Interfaces;
using task_mananger_api.Infrastructure.Persistence;

namespace task_mananger_api.Infrastructure.Repositories;

public class TaskRepository(AppDbContext context) : ITaskRepository
{
    public async Task<List<TaskEntity>> GetAllAsync()
    {
        return await context.Tasks.ToListAsync();
    }

    public TaskEntity? GetById(int id)
    {
        return context.Tasks.FirstOrDefault(t => t.Id == id);
    }

    public TaskEntity Create(TaskEntity task)
    {
        var saved = context.Tasks.Add(task);
        context.SaveChanges();

        return saved.Entity;
    }

    public void Update(TaskEntity task)
    {
        context.Tasks.Update(task);
        context.SaveChanges();
    }

    public void Delete(TaskEntity task)
    {
        context.Tasks.Remove(task);
        context.SaveChanges();
    }
}