using System.Threading.Tasks;
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
        public async Task<ActionResult<List<ProjectResponseDto>>> GetProjects()
        {
            var result = await _projectService.GetProjects();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponseDto>> GetProject(int id)
        {
            var result = await _projectService.GetProject(id);
            if (result == null)
            {
                return NotFound("Project not found");
            }
            return Ok(result); 
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponseDto>> CreateProject(ProjectRequestDto dto)
        {
            var result = await _projectService.CreateProject(dto);
            return CreatedAtAction(
                nameof(GetProject),
                new { id = result.Id },
                result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProjectRequestDto>> UpdateProject(int id, ProjectRequestDto dto)
        {
            var result = await _projectService.UpdateProject(id, dto);
            if (result == null)
            {
                return NotFound("Project not found");
            }
            
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await _projectService.DeleteProject(id);
            if (!result)
            {
                return NotFound("Project not found");
            }

            return NoContent();
        }
    }
}