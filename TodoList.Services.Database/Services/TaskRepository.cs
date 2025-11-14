using Microsoft.EntityFrameworkCore;
using TodoList.Services.Database.Context;
using TodoList.Services.Interfaces;
using TodoList.WebApi.Models.Helpers;
using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Services;

public class TaskRepository : ITaskRepository
{
    private readonly TodoListDbContext context;

    public TaskRepository(TodoListDbContext context)
    {
        this.context = context;
    }

    public async Task<TaskModel> CreateAsync(int todoListId, TaskCreateModel model)
    {
        var entity = Mapper.ToEntityFromCreate(todoListId, model);
        Console.WriteLine($"[DEBUG] Adding new task for TodoListId={todoListId}");

        await this.context.Tasks.AddAsync(entity);
        await this.context.SaveChangesAsync();

        return Mapper.ToModel(entity);
    }

    public async Task<bool> DeleteAsync(int todoListId, int id)
    {
        var entity = await this.context.Tasks.Where(t => t.Id == id && t.TodoListId == todoListId).FirstOrDefaultAsync();

        if (entity == null)
        {
            return false;
        }

        this.context.Tasks.Remove(entity);
        await this.context.SaveChangesAsync();

        return true;
    }

    public async Task<PaginatedModel<TaskModel>> GetAllAsync(int todoListId, int pageNum, int pageSize)
    {
        var models = await this.context.Tasks.Where(t => t.TodoListId == todoListId).ToListAsync();

        return Mapper.ToPaginatedModel(models.Skip((pageNum - 1) * pageSize).Take(pageSize).Select(entity => Mapper.ToModel(entity)), models.Count, pageNum, pageSize);
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync(int todoListId)
    {
        var entities = await this.context.Tasks.Where(t => t.TodoListId == todoListId).ToListAsync();

        return entities.Select(entity => Mapper.ToModel(entity));
    }

    public async Task<TaskModel?> GetAsync(int todoListId, int id)
    {
        var entity = await this.context.Tasks.Where(t => t.Id == id && t.TodoListId == todoListId).FirstOrDefaultAsync();

        if (entity == null)
        {
            return null;
        }

        return Mapper.ToModel(entity);
    }

    public async Task<bool> UpdateAsync(int todoListId, int id, TaskCreateModel model)
    {
        var entity = await this.context.Tasks.Where(t => t.Id == id && t.TodoListId == todoListId).FirstOrDefaultAsync();

        if (entity == null)
        {
            return false;
        }

        Mapper.UpdateEntity(entity, model);
        await this.context.SaveChangesAsync();

        return true;
    }
}
