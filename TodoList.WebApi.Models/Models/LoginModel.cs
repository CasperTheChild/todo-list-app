using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TodoList.WebApi.Models.Models;

public class LoginModel
{
    [Required]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string Password { get; set; } = string.Empty;
}
