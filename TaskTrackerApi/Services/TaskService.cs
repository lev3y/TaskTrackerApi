using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services
{
    public class TaskService
    {
        public List<TaskResponseDto>? GetTasks(int projectId)
        {
            var project = ProjectStore.Projects.FirstOrDefault(p => p.Id == projectId);
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
            var task = ProjectStore.Projects
                .SelectMany(p => p.Tasks)
                .FirstOrDefault(t => t.Id == taskId);
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
            var project = ProjectStore.Projects.FirstOrDefault(p => p.Id == projectId);
            if (project == null)
            {
                return null;
            }

            int newId = ProjectStore.Projects
                .SelectMany(p => p.Tasks)
                .Any()
                    ? ProjectStore.Projects.SelectMany(p => p.Tasks).Max(p => p.Id) + 1
                    : 1;

            var task = new TaskItem
            {
                Id = newId,
                ProjectId = projectId,
                Title = dto.Title,
                Description = dto.Description,
                Status = dto.Status,
                DueDate = dto.DueDate,
                CreatedAt = DateTime.UtcNow,
            };

            project.Tasks.Add(task);

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
            var task = ProjectStore.Projects
                .SelectMany(p => p.Tasks)
                .FirstOrDefault(t => t.Id == taskId);
            if (task == null)
            {
                return null;
            }

            task.Title = dto.Title.Trim();
            task.Description = dto.Description;
            task.Status = dto.Status;
            task.DueDate = dto.DueDate;

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
    }
}