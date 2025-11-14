using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Models.Models;

public class TaskCreateModel
{
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(250)]
    public string? Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } = DateTime.Today;

    public DateTime? EndDate { get; set; }

    public bool IsCompleted { get; set; } = false;
}
