using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StitchStack.Data.Repositories;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class EditFabricModel : PageModel
    {
        private readonly IFabricRepository _fabricRepository;

        public EditFabricModel(IFabricRepository fabricRepository)
        {
            _fabricRepository = fabricRepository;
        }

        [BindProperty]
        public Fabric Fabric { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var fabric = await _fabricRepository.GetByIdAsync(id);
            if (fabric == null)
            {
                return NotFound();
            }

            Fabric = fabric;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _fabricRepository.UpdateAsync(Fabric);
            return RedirectToPage("Fabrics");
        }
    }
}
