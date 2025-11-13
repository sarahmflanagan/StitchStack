using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StitchStack.Data.Repositories;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class AddPatternModel : PageModel
    {
        private readonly IPatternRepository _patternRepository;

        public AddPatternModel(IPatternRepository patternRepository)
        {
            _patternRepository = patternRepository;
        }

        [BindProperty]
        public Pattern Pattern { get; set; } = new();

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _patternRepository.AddAsync(Pattern);
            return RedirectToPage("Patterns");
        }
    }
}
