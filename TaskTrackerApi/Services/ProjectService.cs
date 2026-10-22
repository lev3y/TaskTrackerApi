using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services
{
    public class ProjectService
    {
        private static List<Project> projects = new()
        {
            new Project
            {
                Id = 1,
                Name = "Website Redesign",
                Description = "Redesign the company website",
                Tasks = new List<TaskItem>
                {
                    new TaskItem
                    {
                        Id = 1,
                        ProjectId = 1,
                        Title = "Create wireframes",
                        Status = TaskItemStatus.Todo
                    },
                    new TaskItem
                    {
                        Id = 2,
                        ProjectId = 1,
                        Title = "Design Homepage",
                        Status = TaskItemStatus.InProgress
                    }
                }
            },
            new Project
            {
                Id = 2,
                Name = "Mobile App",
                Description = "Develop mobile application",
                Tasks = new List<TaskItem>
                {
                    new TaskItem
                    {
                        Id = 3,
                        ProjectId = 2,
                        Title = "Set up project",
                        Status = TaskItemStatus.Done
                    },
                    new TaskItem
                    {
                        Id = 4,
                        ProjectId = 2,
                        Title = "Build login screen",
                        Status = TaskItemStatus.Todo
                    }
                }
            }
        };

        public List<ProjectResponseDto> GetProjects()
        {
            var result = projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt
            }).ToList();

            return result;
        }

        public ProjectResponseDto? GetProject(int id)
        {
            var result = projects.FirstOrDefault(p => p.Id == id);
            if (result == null)
            {
                return null;
            }

            return new ProjectResponseDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedAt = result.CreatedAt
            };
        }
    }
}