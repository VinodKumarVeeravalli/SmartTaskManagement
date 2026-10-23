using Microsoft.AspNetCore.Mvc;
using STM.Application.Interfaces.Services;
using STM.Domain.Entities;

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
        public async Task<ActionResult<List<Project>>> GetAllProjectsAsync()
        {
            var projects = await _projectService.GetAllProjectsAsync();

            return Ok(projects);
        }
    }
}
