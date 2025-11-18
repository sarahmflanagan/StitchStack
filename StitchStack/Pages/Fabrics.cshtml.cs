using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StitchStack.Data.Repositories;
using StitchStack.Models;

namespace StitchStack.Pages
{
    public class FabricsModel : PageModel
    {
        private readonly IFabricRepository _fabricRepository;

        public FabricsModel(IFabricRepository fabricRepository)
        {
            _fabricRepository = fabricRepository;
        }

        public IList<Fabric> Fabrics { get; set; } = new List<Fabric>();

        public async Task OnGetAsync()
        {
            var fabrics = await _fabricRepository.GetFabricsAsync();
            Fabrics = fabrics.ToList();
        }

        public async Task<IActionResult> OnGetDeleteAsync(int id)
        {
            await _fabricRepository.DeleteFabricAsync(id);
            return RedirectToPage();
        }
    }
}
