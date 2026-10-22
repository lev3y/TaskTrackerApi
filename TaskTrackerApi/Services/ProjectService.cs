using TaskTrackerApi.DTOs;

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
    }
}