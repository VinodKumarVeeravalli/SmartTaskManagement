using Mapster;
using STM.Application.DTO;
using STM.Application.Interfaces.Repositories;
using STM.Application.Interfaces.Services;
using STM.Domain.Entities;

namespace STM.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<List<ProjectResponseDto>> GetAllProjectsAsync()
        {
            var projects = await _projectRepository.GetAllProjectsAsync();
            return projects.Adapt<List<ProjectResponseDto>>();
        }

        public async Task<ProjectResponseDto> GetProjectByIdAsync(int id)
        {
            var project = await _projectRepository.GetProjectByIdAsync(id);
            return project.Adapt<ProjectResponseDto>();
        }

        public async Task<bool> DeleteProjectByIdAsync(int id)
        {
            return await _projectRepository.DeleteProjectByIdAsync(id);
        }

        public async Task<ProjectResponseDto> AddProjectAsync(AddProjectDto project, string userId)
        {
            var projectEntity = project.Adapt<Project>();

            projectEntity.CreatedDate = DateTime.UtcNow;
            projectEntity.CreatedBy = userId;
            var addedProject = await _projectRepository.AddProjectAsync(projectEntity);
            return addedProject.Adapt<ProjectResponseDto>();
        }
    }
}
