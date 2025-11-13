using StitchStack.Data.Repositories;
using StitchStack.Models;

namespace StitchStack.Data.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly IFabricRepository _fabricRepository;
    private readonly IPatternRepository _patternRepository;
    public ProjectService(IProjectRepository projectRepository, IFabricRepository fabricRepository, IPatternRepository patternRepository)
    {
        _projectRepository = projectRepository;
        _fabricRepository = fabricRepository;
        _patternRepository = patternRepository;
    }
    
    public async Task<IEnumerable<Project>> GetProjectsAsync()
    {
        return await _projectRepository.GetProjectsAsync();
    }

    public async Task<Project?> GetProjectByIdAsync(int id)
    {
        var result = await _projectRepository.GetProjectByIdAsync(id);
        return result ?? null;
    }

    public async Task AddProjectAsync(Project project)
    {
        if (project.PatternId != null)
        {
            var patternId = project.PatternId.Value;
            var pattern = _patternRepository.GetPatternByIdAsync(patternId);
            if (pattern.Result == null)
            {
                throw new BadHttpRequestException("Pattern not found");
            }
        }
        if (project.FabricId != null)
        {
            var fabricId = project.FabricId.Value;
            var fabric = _fabricRepository.GetFabricByIdAsync(fabricId);
            if (fabric.Result == null)
            {
                throw new BadHttpRequestException("Fabric not found");
            }
        }
        await  _projectRepository.AddProjectAsync(project);
    }

    public async Task UpdateProjectAsync(int id, Project project)
    {
        await  _projectRepository.UpdateProjectAsync(id, project);
    }

    public async Task DeleteProjectAsync(int id)
    {
        await  _projectRepository.DeleteProjectAsync(id);
    }
}