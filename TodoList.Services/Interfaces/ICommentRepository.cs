using TodoList.WebApi.Models.Enums;
using TodoList.WebApi.Models.Models;

namespace TodoList.Services.Interfaces;

public interface ICommentRepository
{
    public Task<IEnumerable<CommentModel>> GetAllComments(int taskId, CommentRequestStatus status);

    public Task<PaginatedModel<CommentModel>> GetPagedComments(int taskId, CommentRequestStatus status, int pageNum, int pageSize);

    public Task<CommentModel?> GetCommentById(int commentId);

    public Task<CommentModel> CreateComment(int taskId, CommentCreateModel model);

    public Task<bool> UpdateComment(int commentId, CommentCreateModel model);

    public Task<bool> ChangeStatus(int commentId, CommentStatus status);
}
