using Microsoft.EntityFrameworkCore;
using StitchStack.Data.InMemory;
using StitchStack.Models;

namespace StitchStack.Data.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly InMemoryDBContext _dbContext;
        public ProjectRepository(InMemoryDBContext context)
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
            result.PatternChoice = project.PatternChoice;
            result.FabricChoice = project.FabricChoice;
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
