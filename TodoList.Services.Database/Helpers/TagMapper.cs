using TodoList.Services.Database.Entities;
using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Database.Helpers;

public static class TagMapper
{
    public static TagModel ToModel(TagEntity entity)
    {
        return new TagModel
        {
            Id = entity.Id,
            Name = entity.TagName,
        };
    }

    public static TagEntity ToEntity(TagModel model)
    {
        return new TagEntity
        {
            Id = model.Id,
            TagName = model.Name,
            NormalizedTagName = model.Name.ToLower(),
        };
    }

    public static TagEntity ToEntity(TagCreateModel model)
    {
        return new TagEntity
        {
            TagName = model.Name,
            NormalizedTagName = model.Name.ToLower(),
        };
    }

    public static TaskTagEntity ToTaskTagEntity(int taskId, int tagId)
    {
        return new TaskTagEntity
        {
            TaskId = taskId,
            TagId = tagId,
        };
    }
}
