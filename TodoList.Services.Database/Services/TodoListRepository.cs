using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
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

    private readonly ICurrentUserService user;

    public TodoListRepository(TodoListDbContext context, ICurrentUserService user)
    {
        this.context = context;
        this.user = user;
    }

    public async Task<TodoListModel> CreateAsync(TodoListCreateModel model)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = Mapper.ToEntityFromCreate(model, userId);

        await this.context.TodoLists.AddAsync(entity);
        await this.context.SaveChangesAsync();

        return Mapper.ToModel(entity);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = await this.context.TodoLists.Where(u => u.UserId == userId && u.Id == id).FirstOrDefaultAsync();

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
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var query = this.context.TodoLists.Where(u => u.UserId == userId);

        var totalItems = await query.CountAsync();

        var entities = await query.Skip((pageNum - 1) * pageSize).Take(pageSize).ToListAsync();

        var res = entities.Select(entity => Mapper.ToModel(entity));

        return Mapper.ToPaginatedModel(res, totalItems, pageNum, pageSize);
    }

    public async Task<IEnumerable<TodoListModel>?> GetAllAsync()
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entities = await this.context.TodoLists.Where(u => u.UserId == userId).ToListAsync();

        return entities.Select(entity => Mapper.ToModel(entity));
    }

    public async Task<PaginatedModel<TodoListPreviewModel>> GetAllPreviewAsync(int pageNum, int pageSize)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entities = await this.context.TodoLists.Where(u => u.UserId == userId).Skip((pageNum - 1) * pageSize).Take(pageSize).Include(t => t.Tasks).ToListAsync();

        var models = entities.Select(t => Mapper.ToPreviewModel(t));

        return Mapper.ToPaginatedModel(models, models.Count(), pageNum, pageSize);
    }

    public async Task<TodoListModel?> GetAsync(int id)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = await this.context.TodoLists.Where(u => u.UserId == userId && u.Id == id).FirstOrDefaultAsync();

        if (entity == null)
        {
            return null;
        }

        return Mapper.ToModel(entity);
    }

    public async Task<TodoListModel?> PatchAsync(int id, JsonPatchDocument<TodoListUpdateModel> patchDoc)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = await this.context.TodoLists.Where(u => u.UserId == userId && u.Id == id).FirstOrDefaultAsync();

        if (entity == null)
        {
            return null;
        }

        var model = new TodoListUpdateModel
        {
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
        };

        patchDoc.ApplyTo(model);
        Mapper.UpdateEntity(entity, model);
        await this.context.SaveChangesAsync();

        return Mapper.ToModel(entity);
    }

    public async Task<bool> UpdateAsync(int id, TodoListCreateModel model)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = await this.context.TodoLists.Where(u => u.UserId == userId && u.Id == id).FirstOrDefaultAsync();

        if (entity == null)
        {
            return false;
        }

        Mapper.UpdateEntity(entity, model);
        await this.context.SaveChangesAsync();

        return true;
    }
}
