using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoList.Services.Interfaces;
using TodoList.WebApi.Models.Models;

namespace TodoList.WebApi.Controllers
{
    [Route("api/TodoList/{todoListId}/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository service;

        public TasksController(ITaskRepository service)
        {
            this.service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskModel>> GetByIdAsync(int todoListId, int id)
        {
            var model =  await this.service.GetAsync(todoListId, id);

            if (model == null)
            {
                return NotFound();
            }

            return Ok(model);
        }

        [HttpGet("paged")]
        public async Task<ActionResult<PaginatedModel<TaskModel>>> GetPagedAsync(int todoListId, int pageNum, int pageSize)
        {
            var models = await this.service.GetAllAsync(todoListId, pageNum, pageSize);

            return Ok(models);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskModel>>> GetAllAsync(int todoListId)
        {
            var models = await this.service.GetAllAsync(todoListId);

            return Ok(models);
        }

        [HttpPatch("{id}")]
        [Consumes("application/json-patch+json")]
        public async Task<ActionResult<TaskModel>> PatchAsync(int todoListId, int id, Microsoft.AspNetCore.JsonPatch.JsonPatchDocument<TaskUpdateModel> patchDoc)
        {
            var entity = await this.service.Patch(todoListId, id, patchDoc);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }

        [HttpPost]
        public async Task<ActionResult<TaskModel>> PostAsync(int todoListId, TaskCreateModel model)
        {
            var entity = await this.service.CreateAsync(todoListId, model);

            return Ok(entity);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteAsync(int todoListId, int id)
        {
            var success = await this.service.DeleteAsync(todoListId, id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateAsync(int todoListId, int id, TaskCreateModel model)
        {
            var success = await this.service.UpdateAsync(todoListId, id, model);

            if (!success)
            {
                return NotFound();
            }

            return Ok();
        }
    }
}
