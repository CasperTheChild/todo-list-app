using System.ComponentModel.DataAnnotations;

namespace TodoList.WebApi.Models.Models;

public class TagCreateModel
{
    [MaxLength(15)]
    public string Name { get; set; } = string.Empty;
}
