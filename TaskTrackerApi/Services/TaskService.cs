using TaskTrackerApi.DTOs;

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
    }
}