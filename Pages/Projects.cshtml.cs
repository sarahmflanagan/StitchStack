using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StitchStack.Data.Repositories;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class ProjectsModel : PageModel
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IFabricRepository _fabricRepository;
        private readonly IPatternRepository _patternRepository;

        public ProjectsModel(
            IProjectRepository projectRepository,
            IFabricRepository fabricRepository,
            IPatternRepository patternRepository)
        {
            _projectRepository = projectRepository;
            _fabricRepository = fabricRepository;
            _patternRepository = patternRepository;
        }

        public IList<Project> Projects { get; set; } = new List<Project>();

        public async Task OnGetAsync()
        {
            Projects = await _projectRepository.GetAllAsync();
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            await _projectRepository.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
