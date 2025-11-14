using Microsoft.EntityFrameworkCore;
using System.Linq;
using TodoList.Services.Database.Context;
using TodoList.Services.Interfaces;
using TodoList.WebApi.Models.Helpers;
using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Services;

public class TodoListRepository : ITodoListRepository
{
    private readonly TodoListDbContext context;

    public TodoListRepository(TodoListDbContext context)
    {
        this.context = context;
    }

    public async Task<TodoListModel> CreateAsync(TodoListCreateModel model)
    {
        var entity = Mapper.ToEntityFromCreate(model);

        await this.context.TodoLists.AddAsync(entity);
        await this.context.SaveChangesAsync();

        return Mapper.ToModel(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await this.context.TodoLists.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        this.context.TodoLists.Remove(entity);
        await this.context.SaveChangesAsync();

        return true;
    }

    public async Task<PaginatedModel<TodoListModel>?> GetAllAsync(int pageNum, int pageSize)
    {
        var entities = await this.context.TodoLists.ToListAsync();

        var res = entities.Select(entity => Mapper.ToModel(entity)).Skip((pageNum - 1) * pageSize).Take(pageSize);

        return Mapper.ToPaginatedModel(res, entities.Count, pageNum, pageSize);
    }

    public async Task<IEnumerable<TodoListModel>?> GetAllAsync()
    {
        var entities = await this.context.TodoLists.ToListAsync();

        return entities.Select(entity => Mapper.ToModel(entity));
    }

    public async Task<PaginatedModel<TodoListPreviewModel>> GetAllPreviewAsync(int pageNum, int pageSize)
    {
        var entities = await this.context.TodoLists.Take(pageSize).Include(t => t.Tasks).ToListAsync();

        var models = entities.Select(t => Mapper.ToPreviewModel(t));

        return Mapper.ToPaginatedModel(models, models.Count(), pageNum, pageSize);
    }

    public async Task<TodoListModel?> GetAsync(int id)
    {
        var entity = await this.context.TodoLists.FindAsync(id);

        if (entity == null)
        {
            return null;
        }

        return Mapper.ToModel(entity);
    }

    public async Task<bool> UpdateAsync(int id, TodoListCreateModel model)
    {
        var entity = await this.context.TodoLists.FindAsync(id);

        if (entity == null)
        {
            return false;
        }

        Mapper.UpdateEntity(entity, model);
        await this.context.SaveChangesAsync();

        return true;
    }
}
