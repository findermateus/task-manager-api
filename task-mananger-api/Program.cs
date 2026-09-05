using Microsoft.EntityFrameworkCore;
using task_mananger_api.Domain.Interfaces;
using task_mananger_api.Domain.UseCases;
using task_mananger_api.Infrastructure.Persistence;
using task_mananger_api.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=tasks.db"));

builder.Services.AddControllers();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<GetAllTasks>();
builder.Services.AddScoped<CreateTask>();
builder.Services.AddScoped<StartTask>();
builder.Services.AddScoped<GetTaskById>();
builder.Services.AddScoped<CancelTask>();
builder.Services.AddScoped<CompleteTask>();
builder.Services.AddScoped<DeleteTask>();
builder.Services.AddScoped<UpdateTask>();

var app = builder.Build();

app.UseExceptionHandler();
app.UseStatusCodePages();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();