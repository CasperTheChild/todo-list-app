using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Interfaces;

public interface ITaskRepository
{
    Task<TaskModel?> GetAsync(int todoListId, int id);

    Task<TaskModel> CreateAsync(int todoListId, TaskCreateModel model);

    Task<bool> UpdateAsync(int todoListId, int id, TaskCreateModel model);

    Task<bool> DeleteAsync(int todoListId, int id);

    Task<IEnumerable<TaskModel>> GetAllAsync(int todoListId);

    Task<PaginatedModel<TaskModel>> GetAllAsync(int todoListId, int pageNum, int pageSize);
}
