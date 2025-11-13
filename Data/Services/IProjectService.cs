using StitchStack.Models;

namespace StitchStack.Data.Services;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetProjectsAsync();
    Task<Project?> GetProjectByIdAsync(int id);
    Task AddProjectAsync(Project project);
    Task UpdateProjectAsync(int id, Project project);
    Task DeleteProjectAsync(int id);
}