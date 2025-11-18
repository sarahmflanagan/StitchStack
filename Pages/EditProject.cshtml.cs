using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StitchStack.Data.Repositories;
using StitchStack.Data.Services;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class EditProjectModel : PageModel
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IProjectService _projectService;
        private readonly IFabricRepository _fabricRepository;
        private readonly IPatternRepository _patternRepository;

        public EditProjectModel(
            IProjectRepository projectRepository,
            IProjectService projectService,
            IFabricRepository fabricRepository,
            IPatternRepository patternRepository)
        {
            _projectRepository = projectRepository;
            _projectService = projectService;
            _fabricRepository = fabricRepository;
            _patternRepository = patternRepository;
        }

        [BindProperty]
        public Project Project { get; set; } = new() { Id = 0 };

        public SelectList? FabricList { get; set; }
        public SelectList? PatternList { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var project = await _projectRepository.GetProjectByIdAsync(id.Value);
            if (project == null)
            {
                return NotFound();
            }

            Project = project;
            await LoadDropdowns();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload the original entity first
            var originalProject = await _projectRepository.GetProjectByIdAsync(Project.Id);
            if (originalProject != null)
            {
                // For any field that's empty/null, restore from original before validation
                if (string.IsNullOrEmpty(Project.Name))
                    Project.Name = originalProject.Name;
                if (Project.FabricId == null || Project.FabricId == 0)
                    Project.FabricId = originalProject.FabricId;
                if (Project.PatternId == null || Project.PatternId == 0)
                    Project.PatternId = originalProject.PatternId;
                if (Project.ToilRequired == null)
                    Project.ToilRequired = originalProject.ToilRequired;
                if (string.IsNullOrEmpty(Project.Description))
                    Project.Description = originalProject.Description;
            }

            // Now validate - but only the fields that were actually posted
            // Clear ModelState completely and rebuild it manually for posted fields only
            var postedKeys = Request.Form.Keys.Where(k => k.StartsWith("Project.")).ToList();
            var keysToRemove = ModelState.Keys.Where(k => k.StartsWith("Project.") && !postedKeys.Contains(k)).ToList();

            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }

            if (!ModelState.IsValid)
            {
                await LoadDropdowns();
                return Page();
            }

            try
            {
                await _projectRepository.UpdateProjectAsync(Project.Id, Project);
                return RedirectToPage("/Projects");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating project: {ex.Message}");
                await LoadDropdowns();
                return Page();
            }
        }

        private async Task LoadDropdowns()
        {
            var fabrics = await _fabricRepository.GetFabricsAsync();
            var patterns = await _patternRepository.GetPatternsAsync();

            FabricList = new SelectList(fabrics, "Id", "Type", Project.FabricId);
            PatternList = new SelectList(patterns, "Id", "Name", Project.PatternId);
        }
    }
}
