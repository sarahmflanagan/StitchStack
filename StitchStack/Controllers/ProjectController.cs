using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using StitchStack.Data.Repositories;
using StitchStack.Models;
using System.Threading.Tasks;
using StitchStack.Data.Services;

namespace StitchStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {

            return await _projectService.GetProjectsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectByIdAsync(int id)
        {
            var result = await _projectService.GetProjectByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task AddNewProjectAsync(Project Project)
        {
            await _projectService.AddProjectAsync(Project);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProjectAsync(int id, Project Project)
        {
            if (id == null || Project == null)
            {
                return BadRequest();
            }
            await _projectService.UpdateProjectAsync(id, Project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectAsync(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            await _projectService.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
