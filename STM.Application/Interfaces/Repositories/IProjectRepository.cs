using STM.Domain.Entities;

namespace STM.Application.Interfaces.Repositories
{
    public interface IProjectRepository
    {
        Task<List<Project>> GetAllProjectsAsync();

        Task<Project> GetProjectByIdAsync(int id);
    }
}
