using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Services;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TasksController : ControllerBase
    {
        readonly TaskService _taskService;
        public TasksController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // Override the controller default to match the route, since I want put all task-related endpoint in same controller
        [HttpGet("~/api/projects/{id}/tasks")]
        public ActionResult<List<TaskResponseDto>> GetTasks(int id)
        {
            var result = _taskService.GetTasks(id);
            if (result == null)
            {
                return NotFound("Tasks not found (Project ID does not exist)");
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public ActionResult<TaskResponseDto> GetTask(int id)
        {
            var result = _taskService.GetTask(id);
            if (result == null)
            {
                return NotFound("Task not found");
            }

            return Ok(result);
        }

        [HttpPost("~/api/projects/{id}/tasks")]
        public ActionResult<TaskResponseDto> CreateTask(int id, TaskRequestDto dto)
        {
            if (!Enum.IsDefined(typeof(TaskItemStatus), dto.Status))
            {
                return BadRequest("Invalid task status. Only 'Todo', 'InProgress', 'Done' is accepted");
            }

            var result = _taskService.CreateTask(id, dto);
            if (result == null)
            {
                return NotFound("Project not found");
            }

            return CreatedAtAction(
                nameof(GetTask),
                new { id = result.Id },
                result
            );
        }

        [HttpPut("{id}")]
        public ActionResult<TaskResponseDto> UpdateTask(int id, TaskRequestDto dto)
        {
            if (!Enum.IsDefined(typeof(TaskItemStatus), dto.Status))
            {
                return BadRequest("Invalid task status. Only 'Todo', 'InProgress', 'Done' is accepted");
            }

            var result = _taskService.UpdateTask(id, dto);
            if (result == null)
            {
                return NotFound("Task not found");
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {
            var result = _taskService.DeleteTask(id);
            if (!result)
            {
                return NotFound("Task not found");
            }
            return NoContent();
        } 
    }
}