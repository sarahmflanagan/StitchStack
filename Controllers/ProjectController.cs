using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using StitchStack.Data.Repositories;
using StitchStack.Models;
using System.Threading.Tasks;

namespace StitchStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectController(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Project>> GetAllProjectsAsync()
        {

            return await _projectRepository.GetProjectsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Project>> GetProjectByIdAsync(int id)
        {
            var result = await _projectRepository.GetProjectByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task AddNewProjectAsync(Project Project)
        {
            await _projectRepository.AddProjectAsync(Project);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProjectAsync(int id, Project Project)
        {
            if (id == null || Project == null)
            {
                return BadRequest();
            }
            await _projectRepository.UpdateProjectAsync(id, Project);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProjectAsync(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            await _projectRepository.DeleteProjectAsync(id);
            return NoContent();
        }
    }
}
