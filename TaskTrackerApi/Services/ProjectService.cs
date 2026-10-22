using TaskTrackerApi.Data;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Models;

namespace TaskTrackerApi.Services
{
    public class ProjectService
    {
        private readonly AppDbContext _context;
        public ProjectService(AppDbContext context)
        {
            _context = context;
        }

        public List<ProjectResponseDto> GetProjects()
        {
            var result = _context.Projects.Select(p => new ProjectResponseDto
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
            var result = _context.Projects.FirstOrDefault(p => p.Id == id);
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
                TotalTasks = _context.Tasks.Count(t => t.ProjectId == result.Id)
            };
        }

        public ProjectResponseDto CreateProject(ProjectRequestDto dto)
        {
            var project = new Project
            {
                Name = dto.Name.Trim(),
                Description = dto.Description,
                CreatedAt = DateTime.UtcNow,
            };

            _context.Projects.Add(project);
            _context.SaveChanges();

            return new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                TotalTasks = 0
            };
        }

        public ProjectResponseDto? UpdateProject(int id, ProjectRequestDto dto)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return null;
            }

            project.Name = dto.Name.Trim();
            project.Description = dto.Description;
            _context.SaveChanges();

            return new ProjectResponseDto
            {
                Id = project.Id,
                Name = project.Name,
                Description = project.Description,
                CreatedAt = project.CreatedAt,
                TotalTasks = _context.Tasks.Count(t => t.ProjectId == id)
            };
        }

        public bool DeleteProject(int id)
        {
            var project = _context.Projects.FirstOrDefault(p => p.Id == id);
            if (project == null)
            {
                return false;
            }

            _context.Projects.Remove(project);
            _context.SaveChanges();
            return true;
        }
    }
}