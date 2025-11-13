using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StitchStack.Data.Repositories;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class PatternsModel : PageModel
    {
        private readonly IPatternRepository _patternRepository;

        public PatternsModel(IPatternRepository patternRepository)
        {
            _patternRepository = patternRepository;
        }

        public IList<Pattern> Patterns { get; set; } = new List<Pattern>();

        public async Task OnGetAsync()
        {
            Patterns = await _patternRepository.GetAllAsync();
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            await _patternRepository.DeleteAsync(id);
            return RedirectToPage();
        }
    }
}
