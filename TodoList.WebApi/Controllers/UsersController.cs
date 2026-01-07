using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using TodoList.Services.Interfaces;

namespace TodoList.WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/TodoList/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserRepository service;

    public UsersController(IUserRepository service)
    {
        this.service = service;
    }

    [HttpGet("exists/email/{email}")]
    public async Task<ActionResult<bool>> ExistsByEmailAsync(string email)
    {
         return this.Ok(await service.ExistsByEmailAsync(email));
    }

    [HttpGet("exists/id/{userId}")]
    public async Task<ActionResult<bool>> ExistsByIdAsync(string userId)
    {
       return this.Ok(await service.ExistsByIdAsync(userId));
    }

    [HttpGet("paged")]
    public async Task<IActionResult> GetUsersAsync(int pageNum = 1, int pageSize = 10)
    {
        var users = await service.GetUsersAsync(pageNum, pageSize);
        return this.Ok(users);
    }
}
