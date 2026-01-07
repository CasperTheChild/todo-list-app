using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using TodoList.Services.Enums;
using TodoList.Services.Interfaces;
using TodoList.WebApi.Models.Models;

namespace TodoList.WebApi.Controllers;

[Authorize]
[Route("api/TodoList/[controller]")]
[ApiController]
public class TaskAssignmentsController : ControllerBase
{
    private readonly ITaskAssignmentRepository service;

    public TaskAssignmentsController(ITaskAssignmentRepository service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<ActionResult<PaginatedModel<TaskModel>>> GetPagedAsync([FromQuery] AssignedTaskQuery query)
    {
        var models = await this.service.GetAssignedTasks(query);
        return Ok(models);
    }

    [HttpDelete("{taskId}/Users/{userId}")]
    public async Task<IActionResult> DeleteAsync(string userId, int taskId)
    {
        await this.service.RemoveTaskAssignmentAsync(userId, taskId);
        return NoContent();
    }

    [HttpPost("{taskId}/Users/{userId}")]
    public async Task<IActionResult> PostAsync(string userId, int taskId)
    {
        await this.service.AssignTaskToUserAsync(userId, taskId);
        return Created();
    }
}
