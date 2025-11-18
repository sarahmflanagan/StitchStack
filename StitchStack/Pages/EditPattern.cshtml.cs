using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StitchStack.Data.Repositories;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class EditPatternModel : PageModel
    {
        private readonly IPatternRepository _patternRepository;

        public EditPatternModel(IPatternRepository patternRepository)
        {
            _patternRepository = patternRepository;
        }

        [BindProperty]
        public Pattern Pattern { get; set; } = new() { Id = 0, Name = "" };

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var pattern = await _patternRepository.GetPatternByIdAsync(id.Value);
            if (pattern == null)
            {
                return NotFound();
            }

            Pattern = pattern;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload the original entity first
            var originalPattern = await _patternRepository.GetPatternByIdAsync(Pattern.Id);
            if (originalPattern != null)
            {
                // For any field that's empty/null, restore from original before validation
                if (string.IsNullOrEmpty(Pattern.Name))
                    Pattern.Name = originalPattern.Name;
                if (string.IsNullOrEmpty(Pattern.Type))
                    Pattern.Type = originalPattern.Type;
                if (string.IsNullOrEmpty(Pattern.Brand))
                    Pattern.Brand = originalPattern.Brand;
                if (Pattern.isWoven == null)
                    Pattern.isWoven = originalPattern.isWoven;
                if (Pattern.isToilComplete == null)
                    Pattern.isToilComplete = originalPattern.isToilComplete;
                if (string.IsNullOrEmpty(Pattern.Description))
                    Pattern.Description = originalPattern.Description;
            }

            // Now validate - but only the fields that were actually posted
            // Clear ModelState completely and rebuild it manually for posted fields only
            var postedKeys = Request.Form.Keys.Where(k => k.StartsWith("Pattern.")).ToList();
            var keysToRemove = ModelState.Keys.Where(k => k.StartsWith("Pattern.") && !postedKeys.Contains(k)).ToList();

            foreach (var key in keysToRemove)
            {
                ModelState.Remove(key);
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                await _patternRepository.UpdatePatternAsync(Pattern.Id, Pattern);
                return RedirectToPage("/Patterns");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating pattern: {ex.Message}");
                return Page();
            }
        }
    }
}
