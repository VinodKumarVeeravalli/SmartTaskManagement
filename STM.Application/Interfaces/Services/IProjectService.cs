using STM.Application.DTO;

namespace STM.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<List<ProjectResponseDto>> GetAllProjectsAsync();

        Task<ProjectResponseDto> GetProjectByIdAsync(int id);
    }
}
