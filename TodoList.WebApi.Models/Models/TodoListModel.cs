using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Models.Models;

public class TodoListModel : IModel
{
    public int Id { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime StartDate { get; set; } = DateTime.Today;

    public bool HasOverdueTasks { get; set; }
}
