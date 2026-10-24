using Microsoft.EntityFrameworkCore;
using STM.Application.Interfaces.Repositories;
using STM.Domain.Entities;
using STM.Infrastructure.Persistence;

namespace STM.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly STMDBContext _context;

        public ProjectRepository(STMDBContext context)
        {
            _context = context;
        }

        public async Task<List<Project>> GetAllProjectsAsync()
        {
            return await _context.Projects.AsNoTracking().ToListAsync();
        }

        public async Task<Project> GetProjectByIdAsync(int id)
        {
            return await _context.Projects.FirstOrDefaultAsync(p => p.ProjectId == id);
        }

        public async Task<bool> DeleteProjectByIdAsync(int id)
        {
            var project = await _context.Projects.FindAsync(id);

            if (project is null)
                return false;

            _context.Projects.Remove(project);

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<Project> AddProjectAsync(Project project)
        {
            _context.Projects.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }
    }
}
