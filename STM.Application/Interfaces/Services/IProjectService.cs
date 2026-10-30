using STM.Application.DTO;

namespace STM.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<List<ProjectResponseDto>> GetAllProjectsAsync();

        Task<ProjectResponseDto> GetProjectByIdAsync(int id);

        Task<bool> DeleteProjectByIdAsync(int id);

        Task<ProjectResponseDto> AddProjectAsync(AddProjectDto project, string userId);
    }
}
