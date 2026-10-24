using Microsoft.AspNetCore.Mvc;
using STM.Application.DTO;
using STM.Application.Interfaces.Services;

namespace STM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProjectResponseDto>>> GetAllProjectsAsync()
        {
            var projects = await _projectService.GetAllProjectsAsync();

            return Ok(projects);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectResponseDto>> GetProjectByIdAsync(int id)
        {
            var project = await _projectService.GetProjectByIdAsync(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProjectByIdAsync(int id)
        {
            var result = await _projectService.DeleteProjectByIdAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ProjectResponseDto>> AddProjectAsync([FromBody] AddProjectDto project)
        {
            var result = await _projectService.AddProjectAsync(project);

            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }
    }
}
