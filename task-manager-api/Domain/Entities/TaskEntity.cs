using task_mananger_api.Domain.Exceptions;
using task_mananger_api.DTOs;
using TaskStatus = task_mananger_api.Domain.Enum.TaskStatus;
using System.Text.Json.Serialization;

namespace task_mananger_api.Domain.Entities;

public class TaskEntity
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateOnly ExpectedConclusionDate { get; private set; }
    public DateTime? CompletedAt { get; private set; }

    [JsonConverter(typeof(JsonStringEnumConverter<TaskStatus>))]
    public TaskStatus Status { get; private set; }

    public TaskEntity(string title, string description, DateOnly expectedConclusionDate)
    {
        if (string.IsNullOrWhiteSpace(title))
        {
            throw new InvalidTaskDataException("Title cannot be empty.");
        }

        var utcNow = DateTime.UtcNow;
        var today = DateOnly.FromDateTime(utcNow);

        if (expectedConclusionDate < today)
        {
            throw new InvalidTaskDataException("Expected conclusion date cannot be earlier than today.");
        }

        Title = title;
        Description = description;
        ExpectedConclusionDate = expectedConclusionDate;
        CreatedAt = utcNow;
        Status = TaskStatus.Pending;
    }

    public static TaskEntity FromPayload(CreateTaskDto payload)
    {
        return new TaskEntity(payload.Title, payload.Description, payload.ExpectedConclusionDate);
    }

    public void Update(UpdateTaskDto payload)
    {
        if (string.IsNullOrWhiteSpace(payload.Title))
        {
            throw new InvalidTaskDataException("Title cannot be empty.");
        }

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        if (payload.ExpectedConclusionDate < today)
        {
            throw new InvalidTaskDataException("Expected conclusion date cannot be earlier than today.");
        }

        Title = payload.Title;
        Description = payload.Description;
        ExpectedConclusionDate = payload.ExpectedConclusionDate;
    }

    public void Start()
    {
        if (Status != TaskStatus.Pending)
        {
            throw new InvalidTaskStateException("Only pending tasks can be started.");
        }

        Status = TaskStatus.InProgress;
    }

    public void Complete()
    {
        if (Status != TaskStatus.Pending && Status != TaskStatus.InProgress)
        {
            throw new InvalidTaskStateException("Only pending or in-progress tasks can be completed.");
        }

        Status = TaskStatus.Completed;
        CompletedAt = DateTime.UtcNow;
    }

    public void Cancel()
    {
        if (Status == TaskStatus.Canceled)
        {
            throw new InvalidTaskStateException("Task already cancelled.");
        }

        Status = TaskStatus.Canceled;
    }
}