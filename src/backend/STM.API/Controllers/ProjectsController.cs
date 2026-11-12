using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using STM.Application.DTO;
using STM.Application.Interfaces.Services;
using System.Security.Claims;

namespace STM.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin,Manager")]
        [HttpPost]
        public async Task<ActionResult<ProjectResponseDto>> AddProjectAsync([FromBody] AddProjectDto project)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? User.FindFirstValue("sub");

            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized();
            }

            var result = await _projectService.AddProjectAsync(project,userId);

            return Ok(result);
        }
    }
}
