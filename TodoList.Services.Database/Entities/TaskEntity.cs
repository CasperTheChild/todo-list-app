using System.ComponentModel.DataAnnotations;

namespace TodoList.Services.Database.Entities;

public class TaskEntity
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } = DateTime.Today;

    public DateTime? EndDate { get; set; }

    public bool IsCompleted { get; set; }

    public int TodoListId { get; set; }

    public TodoListEntity? TodoList { get; set; }
}
