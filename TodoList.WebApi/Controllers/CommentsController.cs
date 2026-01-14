using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Services.Interfaces;
using TodoList.WebApi.Models.Enums;
using TodoList.WebApi.Models.Models;

namespace TodoList.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class CommentsController : ControllerBase
{
    private readonly ICommentRepository service;

    public CommentsController(ICommentRepository service)
    {
        this.service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetComments(int taskId)
    {
        var comments = await this.service.GetAllComments(taskId, CommentRequestStatus.All);
        return Ok(comments);
    }

    [HttpGet("paged")]
    public async Task<ActionResult<PaginatedModel<CommentModel>>> GetPagedComments(int taskId, CommentRequestStatus status ,int pageNum = 1, int pageSize = 10)
    {
        var pagedComments = await this.service.GetPagedComments(taskId, status, pageNum, pageSize);
        return Ok(pagedComments);
    }

    [HttpGet("taskId")]
    public async Task<IActionResult> GetCommentById(int commentId)
    {
        var comment = await this.service.GetCommentById(commentId);
        if (comment == null)
        {
            return NotFound();
        }
        return Ok(comment);
    }

    [HttpPost]
    public async Task<IActionResult> CreateComment(int taskId, [FromBody] CommentCreateModel model)
    {
        var createdComment = await this.service.CreateComment(taskId, model);
        return CreatedAtAction(nameof(GetCommentById), new { commentId = createdComment.Id }, createdComment);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateComment(int commentId, [FromBody] CommentCreateModel model)
    {
        var updated = await this.service.UpdateComment(commentId, model);
        if (!updated)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpPut("status")]
    public async Task<IActionResult> ChangeCommentStatus(int commentId, CommentStatus status)
    {
        var changed = await this.service.ChangeStatus(commentId, status);
        if (!changed)
        {
            return NotFound();
        }
        return NoContent();
    }

}
