using Microsoft.AspNetCore.Mvc.RazorPages;
using TodoList.Services.Enums;
using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Interfaces;

public interface ITaskAssignmentRepository
{
    Task<PaginatedModel<TaskModel>> GetAssignedTasks(AssignedTaskQuery query);

    Task AssignTaskToUserAsync(string userId, int taskId);

    Task RemoveTaskAssignmentAsync(string userId, int taskId);
}
