using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Interfaces;

public interface IUserRepository
{
    public Task<bool> ExistsByIdAsync(string userId);

    public Task<bool> ExistsByEmailAsync(string email);

    public Task<PaginatedModel<UserSummaryModel>> GetUsersAsync(int pageNumber, int pageSize);
}
