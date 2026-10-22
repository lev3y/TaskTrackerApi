using Microsoft.AspNetCore.Mvc;
using TaskTrackerApi.DTOs;
using TaskTrackerApi.Services;

namespace TaskTrackerApi.Controllers
{
    [ApiController]
    [Route("api/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly ProjectService _projectService;
        public ProjectsController(ProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public ActionResult<List<ProjectResponseDto>> GetProjects()
        {
            var result = _projectService.GetProjects();
            return Ok(result);
        }

        [HttpGet("id")]
        public ActionResult<ProjectResponseDto> GetProject(int id)
        {
            var result = _projectService.GetProject(id);
            if (result == null)
            {
                return NotFound("Project not found");
            }
            return Ok(result); 
        }

        [HttpPost]
        public ActionResult<ProjectResponseDto> CreateProject(ProjectRequestDto dto)
        {
            var result = _projectService.CreateProject(dto);
            return CreatedAtAction(
                nameof(GetProject),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id}")]
        public ActionResult<ProjectRequestDto> UpdateProject(int id, ProjectRequestDto dto)
        {
            var result = _projectService.UpdateProject(id, dto);
            if (result == null)
            {
                return NotFound("Project not found");
            }
            
            return Ok(result);
        }
    }
}