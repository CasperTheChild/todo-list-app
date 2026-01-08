using TodoList.Services.Database.Entities;
using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Database.Helpers;

public static class TaskAssignmentMapper
{
    public static TaskAssignmentEntity ToTaskAssignmentEntity(int taskId, string userId)
    {
        return new TaskAssignmentEntity
        {
            TaskId = taskId,
            UserId = userId,
        };
    }
}
