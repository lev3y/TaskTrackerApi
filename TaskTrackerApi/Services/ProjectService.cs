using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services
{
    public class ProjectService
    {
        public List<ProjectResponseDto> GetProjects()
        {
            var result = ProjectStore.Projects.Select(p => new ProjectResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreatedAt = p.CreatedAt,
                TotalTasks = p.Tasks.Count
            }).ToList();

            return result;
        }

        public ProjectResponseDto? GetProject(int id)
        {
            var result = ProjectStore.Projects.FirstOrDefault(p => p.Id == id);
            if (result == null)
            {
                return null;
            }

            return new ProjectResponseDto
            {
                Id = result.Id,
                Name = result.Name,
                Description = result.Description,
                CreatedAt = result.CreatedAt,
                TotalTasks = result.Tasks.Count
            };
        }

        public ProjectResponseDto CreateProject(ProjectRequestDto dto)
        {
            // Find the larget id in project then increase 1, if none found then use 1
            int newId = ProjectStore.Projects.Any()
                ? ProjectStore.Projects.Max(p => p.Id) + 1
                : 1;

            var project = new Project
            {
                Id = newId,
                Name = dto.Name.Trim(),
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
            };

            ProjectStore.Projects.Add(project);

            return new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                TotalTasks = project.Tasks.Count
            };
        }

        public ProjectResponseDto? UpdateProject(int id, ProjectRequestDto dto)
        {
            var project = ProjectStore.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return null;
            }

            project.Name = dto.Name.Trim();
            project.Description = dto.Description;

            return new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                TotalTasks = project.Tasks.Count
            };
        }
    }
}