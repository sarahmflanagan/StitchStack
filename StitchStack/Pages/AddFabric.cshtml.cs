using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StitchStack.Data.Repositories;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class AddFabricModel : PageModel
    {
        private readonly IFabricRepository _fabricRepository;

        public AddFabricModel(IFabricRepository fabricRepository)
        {
            _fabricRepository = fabricRepository;
        }

        [BindProperty]
        public Fabric Fabric { get; set; } = new() { Id = 0, Type = "" };

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _fabricRepository.AddFabricAsync(Fabric);
            return RedirectToPage("/Fabrics");
        }
    }
}
