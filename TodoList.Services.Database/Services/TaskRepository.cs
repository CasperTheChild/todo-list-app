using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoList.Services.Database.Context;
using TodoList.Services.Interfaces;
using TodoList.WebApi.Models.Helpers;
using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Services;

public class TaskRepository : ITaskRepository
{
    private readonly TodoListDbContext context;

    private readonly ICurrentUserService user;

    public TaskRepository(TodoListDbContext context, ICurrentUserService user)
    {
        this.context = context;
        this.user = user;
    }

    public async Task<TaskModel> CreateAsync(int todoListId, TaskCreateModel model)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = Mapper.ToEntityFromCreate(todoListId, model, userId);

        await this.context.Tasks.AddAsync(entity);
        await this.context.SaveChangesAsync();

        var taskAssignmentEntity = Mapper.ToTaskAssignmentEntity(entity.Id, userId);
        await this.context.TaskAssignments.AddAsync(taskAssignmentEntity);
        await this.context.SaveChangesAsync();

        return Mapper.ToModel(entity);
    }

    public async Task<bool> DeleteAsync(int todoListId, int id)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = await this.context.Tasks.Where(t => t.Id == id && t.TodoListId == todoListId && userId == t.UserId).FirstOrDefaultAsync();

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
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var models = await this.context.Tasks.Where(t => t.TodoListId == todoListId && t.UserId == userId).ToListAsync();

        return Mapper.ToPaginatedModel(models.Skip((pageNum - 1) * pageSize).Take(pageSize).Select(entity => Mapper.ToModel(entity)), models.Count, pageNum, pageSize);
    }

    public async Task<IEnumerable<TaskModel>> GetAllAsync(int todoListId)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entities = await this.context.Tasks.Where(t => t.TodoListId == todoListId && t.UserId == userId).ToListAsync();

        return entities.Select(entity => Mapper.ToModel(entity));
    }

    public async Task<TaskModel?> GetAsync(int todoListId, int id)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = await this.context.Tasks.Where(t => t.Id == id && t.TodoListId == todoListId && t.UserId == userId).FirstOrDefaultAsync();

        if (entity == null)
        {
            return null;
        }

        return Mapper.ToModel(entity);
    }

    public async Task<TaskModel?> Patch(int todoListId, int id, JsonPatchDocument<TaskUpdateModel> patchDoc)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = this.context.Tasks.Where(t => t.Id == id && t.TodoListId == todoListId && t.UserId == userId).FirstOrDefault();

        if (entity == null)
        {
            return null;
        }

        var model = new TaskUpdateModel();

        patchDoc.ApplyTo(model);
        Mapper.UpdateEntity(entity, model);

        await this.context.SaveChangesAsync();

        return Mapper.ToModel(entity);
    }

    public async Task<bool> UpdateAsync(int todoListId, int id, TaskCreateModel model)
    {
        var userId = this.user.UserId ?? throw new InvalidOperationException("User ID cannot be null.");

        var entity = await this.context.Tasks.Where(t => t.Id == id && t.TodoListId == todoListId && t.UserId == userId).FirstOrDefaultAsync();

        if (entity == null)
        {
            return false;
        }

        Mapper.UpdateEntity(entity, model);
        await this.context.SaveChangesAsync();

        return true;
    }
}
