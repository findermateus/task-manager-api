namespace task_mananger_api.DTOs;

public class CreateTaskDto
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required DateOnly ExpectedConclusionDate { get; set; }
}