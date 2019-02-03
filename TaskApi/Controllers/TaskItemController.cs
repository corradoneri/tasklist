using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskList.Core.Models;
using TaskList.Core.Logging;
using TaskList.Core.DataAccess;
using System;

namespace TaskList.TaskApi.Controllers
{
	[Route("api/taskitems")]
	[ApiController]
	public class TaskItemController : ControllerBase
	{
		private readonly ILogger _logger;
		private readonly ITaskItemRepository _taskItemRepository;

		public TaskItemController(ILogger logger, ITaskItemRepository taskItemRepository)
		{
			_logger = logger;
			_taskItemRepository = taskItemRepository;
		}

		[HttpGet]
		public async Task<IEnumerable<TaskItem>> Get()
		{
			await _logger.LogInfo("TaskItemController.Get()");
			try
			{
				var taskItems = await _taskItemRepository.Get();
				await _logger.LogInfo("200 OK");
				return taskItems;
			}
			catch (Exception exception)
			{
				await _logger.LogException(exception);
				throw;
			}
		}

		// GET: api/taskitems/5
		[HttpGet("{id}", Name = "Get")]
		public async Task<IActionResult> Get(string id)
		{
			await _logger.LogInfo($"TaskItemController.Get({id})");

			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				var taskItem = await _taskItemRepository.Get(id);
				if (taskItem == null)
				{
					await _logger.LogInfo("404 NOT FOUND ERROR");
					return NotFound();
				}

				await _logger.LogInfo("200 OK");
				return Ok(taskItem);
			}
			catch (Exception exception)
			{
				await _logger.LogException(exception);
				throw;
			}
		}

		// POST: api/taskitems
		[HttpPost]
		public async Task<IActionResult> Post([FromBody] TaskItem taskItem)
		{
			await _logger.LogInfo($"TaskItemController.Post({taskItem})");

			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				await _taskItemRepository.Create(taskItem);
				await _logger.LogInfo("201 CREATED");
				return CreatedAtAction(nameof(Post), new { id = taskItem.Id }, taskItem);
			}
			catch (Exception exception)
			{
				await _logger.LogException(exception);
				throw;
			}
		}

		// PUT: api/taskitems/5
		[HttpPut("{id}")]
		public async Task<IActionResult> Put(string id, [FromBody] TaskItem taskItem)
		{
			await _logger.LogInfo($"TaskItemController.Put({id}, {taskItem})");

			try
			{
				if (!ModelState.IsValid)
					return BadRequest(ModelState);

				if (id != taskItem.Id)
				{
					await _logger.LogInfo($"400 BAD REQUEST");
					return BadRequest();
				}

				await _taskItemRepository.Update(taskItem);
				await _logger.LogInfo($"204 NO CONTENT");
				return NoContent();
			}
			catch (Exception exception)
			{
				await _logger.LogException(exception);
				throw;
			}
		}

		// DELETE: api/taskitems/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(string id)
		{
			await _logger.LogInfo($"TaskItemController.Delete({id})");

			try
			{
				if (!ModelState.IsValid)
				{
					await _logger.LogInfo("400 BAD REQUEST");
					return BadRequest(ModelState);
				}

				var taskItem = await _taskItemRepository.Get(id);
				if (taskItem == null)
				{
					await _logger.LogInfo("404 NOT FOUND");
					return NotFound();
				}

				await _taskItemRepository.Remove(id);
				await _logger.LogInfo("204 NO CONTENT");
				return NoContent();
			}
			catch (Exception exception)
			{
				await _logger.LogException(exception);
				throw;
			}
		}
	}
}
