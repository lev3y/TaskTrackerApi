using Microsoft.EntityFrameworkCore;
using TaskTrackerApi.Data;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services
{
    public class TaskService
    {
        private readonly AppDbContext _context;
        public TaskService(AppDbContext context)
        {
            _context = context;
        }

        public List<TaskResponseDto>? GetTasks(int projectId)
        {
            var project = _context.Projects
                .Include(p => p.Tasks)
                .FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return null;
            }

            var result = project.Tasks.Select(t => new TaskResponseDto
                {
                    Id = t.Id,
                    ProjectId = t.ProjectId,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status.ToString(),
                    DueDate = t.DueDate,
                    CreatedAt = t.CreatedAt
                }).ToList();

            return result;
        }

        public TaskResponseDto? GetTask(int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return null;
            }

            return new TaskResponseDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt
            };
        }

        public TaskResponseDto? CreateTask(int projectId, TaskRequestDto dto)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return null;
            }

            var task = new TaskItem
            {
                ProjectId = projectId,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                DueDate = dto.DueDate,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return new TaskResponseDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt
            };
        }

        public TaskResponseDto? UpdateTask(int taskId, TaskRequestDto dto)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return null;
            }

            task.Title = dto.Title.Trim();
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;
            _context.SaveChanges();

            return new TaskResponseDto
            {
                Id = task.Id,
                ProjectId = task.ProjectId,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status.ToString(),
                DueDate = task.DueDate,
                CreatedAt = task.CreatedAt
            };
        }

        public bool DeleteTask(int taskId)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return false;
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return true;
        }
    }
}