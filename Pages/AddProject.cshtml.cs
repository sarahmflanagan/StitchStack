using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StitchStack.Data.Repositories;
using StitchStack.Data.Services;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class AddProjectModel : PageModel
    {
        private readonly IProjectService _projectService;
        private readonly IFabricRepository _fabricRepository;
        private readonly IPatternRepository _patternRepository;

        public AddProjectModel(
            IProjectService projectService,
            IFabricRepository fabricRepository,
            IPatternRepository patternRepository)
        {
            _projectService = projectService;
            _fabricRepository = fabricRepository;
            _patternRepository = patternRepository;
        }

        [BindProperty]
        public Project Project { get; set; } = new();

        public SelectList? FabricList { get; set; }
        public SelectList? PatternList { get; set; }

        public async Task OnGetAsync()
        {
            await LoadDropdowns();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return Page();
            }

            try
            {
                await _projectService.AddProjectAsync(Project);
                return RedirectToPage("Projects");
            }
            catch (BadHttpRequestException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                await LoadDropdowns();
                return Page();
            }
        }

        private async Task LoadDropdowns()
        {
            var fabrics = await _fabricRepository.GetAllAsync();
            var patterns = await _patternRepository.GetAllAsync();

            FabricList = new SelectList(fabrics, "Id", "Type");
            PatternList = new SelectList(patterns, "Id", "Name");
        }
    }
}
