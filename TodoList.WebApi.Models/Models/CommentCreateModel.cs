using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Models.Models;

public class CommentCreateModel
{
    [Required]
    [Length(1, 150)]
    public string Content { get; set; } = string.Empty;
}
