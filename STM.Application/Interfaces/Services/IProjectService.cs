using STM.Domain.Entities;

namespace STM.Application.Interfaces.Services
{
    public interface IProjectService
    {
        Task<List<Project>> GetAllProjectsAsync();
    }
}
