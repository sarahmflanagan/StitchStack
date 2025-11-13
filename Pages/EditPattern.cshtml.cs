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
        public Pattern Pattern { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var pattern = await _patternRepository.GetByIdAsync(id);
            if (pattern == null)
            {
                return NotFound();
            }

            Pattern = pattern;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _patternRepository.UpdateAsync(Pattern);
            return RedirectToPage("Patterns");
        }
    }
}
