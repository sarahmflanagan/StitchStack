using Microsoft.EntityFrameworkCore;
using StitchStack.Data.SqlDB;
using StitchStack.Models;

namespace StitchStack.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly SqlDBContext _dbContext;
        public ProjectRepository(SqlDBContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            return await _dbContext.Projects.ToListAsync();
        }

        public async Task<Project?> GetProjectByIdAsync(int id)
        {
            var result = await _dbContext.Projects.FindAsync(id);
            return result ?? null;
        }

        public async Task AddProjectAsync(Project project)
        {
            await _dbContext.Projects.AddAsync(project);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProjectAsync(int id, Project project)
        {
            var result = await _dbContext.Projects.FindAsync(id);
            if (result == null)
            {
                throw new Exception();
            }
            result.Name = project.Name;
            result.Description = project.Description;
            result.Pattern = project.Pattern;
            result.Fabric = project.Fabric;
            result.ToilRequired = project.ToilRequired;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProjectAsync(int id)
        {
            var result = await _dbContext.Projects.FindAsync(id);
            if (result == null)
            {
                throw new Exception();
            }

            _dbContext.Projects.Remove(result);
            await _dbContext.SaveChangesAsync();
        }
    }
}
