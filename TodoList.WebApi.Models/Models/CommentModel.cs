using TodoList.WebApi.Models.Enums;

namespace TodoList.WebApi.Models.Models;

public class CommentModel
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; }

    public int TaskId { get; set; }

    public string UserId { get; set; }

    public CommentStatus Status { get; set; }
}
