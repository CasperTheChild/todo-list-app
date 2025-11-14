using System.Runtime.CompilerServices;
using TodoList.Services.Database.Entities;
using TodoList.WebApi.Models.Models;

namespace TodoList.WebApi.Models.Helpers;

public static class Mapper
{
    public static TodoListEntity ToEntityFromCreate(TodoListCreateModel model)
    {
        return new TodoListEntity
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
        };
    }

    public static TaskEntity ToEntityFromCreate(int todoListId, TaskCreateModel model)
    {
        return new TaskEntity
        {
            Title = model.Title,
            Description = model.Description,
            StartDate = model.StartDate,
            EndDate = model.EndDate,
            IsCompleted = model.IsCompleted,
            TodoListId = todoListId,
        };
    }

    public static TodoListModel ToModel(TodoListEntity entity)
    {
        return new TodoListModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
        };
    }

    public static TodoListPreviewModel ToPreviewModel(TodoListEntity entity)
    {
        return new TodoListPreviewModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            Tasks = entity.Tasks != null ? entity.Tasks.Select(t => Mapper.ToModel(t)).ToList() : new List<TaskModel>(),
        };
    }

    public static TaskModel ToModel(TaskEntity entity)
    {
        return new TaskModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            StartDate = entity.StartDate,
            EndDate = entity.EndDate,
            IsCompleted = entity.IsCompleted,
            TodoListId = entity.TodoListId,
        };
    }

    public static PaginatedModel<TodoListModel> ToPaginatedModel(IEnumerable<TodoListModel> models, int totalItems, int pageNum, int pageSize)
    {
        return new PaginatedModel<TodoListModel>
        {
            Items = models,
            TotalItems = totalItems,
            ItemsPerPage = pageSize,
            CurrentPage = pageNum,
        };
    }

    public static PaginatedModel<TodoListPreviewModel> ToPaginatedModel(IEnumerable<TodoListPreviewModel> models, int totalItems, int pageNum, int pageSize)
    {
        return new PaginatedModel<TodoListPreviewModel>
        {
            Items = models,
            TotalItems = totalItems,
            ItemsPerPage = pageSize,
            CurrentPage = pageNum,
        };
    }

    public static PaginatedModel<TaskModel> ToPaginatedModel(IEnumerable<TaskModel> models, int totalItems, int pageNum, int pageSize)
    {
        return new PaginatedModel<TaskModel>
        {
            Items = models,
            TotalItems = totalItems,
            ItemsPerPage = pageSize,
            CurrentPage = pageNum,
        };
    }

    public static void UpdateEntity(TodoListEntity entity, TodoListCreateModel model)
    {
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.StartDate = model.StartDate;
    }

    public static void UpdateEntity(TodoListEntity entity, TodoListUpdateModel model)
    {
        if (model.Title != null)
        {
            entity.Title = model.Title;
        }

        if (model.Description != null)
        {
            entity.Description = model.Description;
        }

        if (model.StartDate != null)
        {
            entity.StartDate = (DateTime)model.StartDate;
        }
    }

    public static void UpdateEntity(TaskEntity entity, TaskCreateModel model)
    {
        entity.Title = model.Title;
        entity.Description = model.Description;
        entity.StartDate = model.StartDate;
        entity.EndDate = model.EndDate;
        entity.IsCompleted = model.IsCompleted;
    }

    public static void UpdateEntity(TaskEntity entity, TaskUpdateModel model)
    {
        if (model.Title != null)
        {
            entity.Title = model.Title;
        }

        if (model.Description != null)
        {
            entity.Description = model.Description;
        }

        if (entity.EndDate != null)
        {
            entity.EndDate = model.EndDate;
        }

        if (model.StartDate != null)
        {
            entity.StartDate = (DateTime)model.StartDate;
        }

        if (model.IsCompleted != null)
        {
            entity.IsCompleted = (bool)model.IsCompleted;
        }
    }
}