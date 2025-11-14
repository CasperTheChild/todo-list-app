using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Models.Models;

public class TodoListUpdateModel
{
    public string? Title { get; set; } = string.Empty;

    public string? Description { get; set; } = string.Empty;

    public DateTime? StartDate { get; set; } = DateTime.Today;
}
