using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Interfaces;

public interface ITodoListRepository
{
    Task<TodoListModel?> GetAsync(int id);

    Task<TodoListModel> CreateAsync(TodoListCreateModel model);

    Task<bool> UpdateAsync(int id, TodoListCreateModel model);

    Task<bool> DeleteAsync(int id);

    Task<IEnumerable<TodoListModel>> GetAllAsync();

    Task<PaginatedModel<TodoListModel>> GetAllAsync(int pageNum, int pageSize);

    Task<PaginatedModel<TodoListPreviewModel>> GetAllPreviewAsync(int pageNum, int pageSize);
}
