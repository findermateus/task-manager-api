using Microsoft.EntityFrameworkCore;
using task_mananger_api.Domain.Entities;

namespace task_mananger_api.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<TaskEntity> Tasks { get; set; }
}