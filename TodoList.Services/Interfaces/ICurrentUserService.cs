namespace TodoList.Services.Interfaces;

public interface ICurrentUserService
{
    string? UserId { get; }
}