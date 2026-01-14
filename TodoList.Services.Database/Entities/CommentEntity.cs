using TodoList.Services.Database.Identity;
using TodoList.WebApi.Models.Enums;

namespace TodoList.Services.Database.Entities;

public class CommentEntity
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public int TaskId { get; set; }

    public TaskEntity Task { get; set; }

    public string UserId { get; set; } = default!;

    public ApplicationUser User { get; set; }

    public CommentStatus Status { get; set; } = CommentStatus.Active;
}
