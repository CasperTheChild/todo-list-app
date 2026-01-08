using TodoList.Services.Database.Identity;
using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Database.Helpers;

public static class UserMapper
{
    public static UserSummaryModel ToModel(ApplicationUser entity)
    {
        return new UserSummaryModel
        {
            Id = entity.Id,
            Email = entity.Email!,
        };
    }
}
