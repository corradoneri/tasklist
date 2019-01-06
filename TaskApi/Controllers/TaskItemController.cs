using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using TaskList.Core.DataAccess;
using TaskList.Core.Models;

namespace TaskList.TaskApi.Controllers
{
    [Route("api/taskitems")]
    [ApiController]
    public class TaskItemController : ControllerBase
    {
        private readonly ITaskItemRepository _taskItemRepository;

        public TaskItemController(ITaskItemRepository taskItemRepository)
        {
            _taskItemRepository = taskItemRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TaskItem>> Get()
        {
            return await _taskItemRepository.Get();
        }

        // GET: api/taskitems/5
        [HttpGet("{id}", Name = "Get")]
        public async Task<IActionResult> Get(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskItem = await _taskItemRepository.Get(id);
            if (taskItem == null)
                return NotFound();

            return Ok(taskItem);
        }

        // POST: api/taskitems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskItem taskItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _taskItemRepository.Create(taskItem);
            return CreatedAtAction(nameof(Post), new { id = taskItem.Id }, taskItem);
        }

        // PUT: api/taskitems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] TaskItem taskItem)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != taskItem.Id)
                return BadRequest();

            await _taskItemRepository.Update(taskItem);
            return NoContent();
        }

        // DELETE: api/taskitems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var taskItem = await _taskItemRepository.Get(id);
            if (taskItem == null)
                return NotFound();
            
            await _taskItemRepository.Remove(id);
            return NoContent();
        }
    }
}
