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
        public Pattern Pattern { get; set; } = new() { Id = 0, Name = "" };

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _patternRepository.AddPatternAsync(Pattern);
            return RedirectToPage("/Patterns");
        }
    }
}
