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
        public Fabric Fabric { get; set; } = new() { Id = 0, Type = "" };

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue || id == 0)
            {
                return BadRequest("Id is required");
            }

            var fabric = await _fabricRepository.GetFabricByIdAsync(id.Value);
            if (fabric == null)
            {
                return NotFound($"Fabric with id {id} not found");
            }

            Fabric = fabric;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Reload the original entity first
            var originalFabric = await _fabricRepository.GetFabricByIdAsync(Fabric.Id);
            if (originalFabric != null)
            {
                // For any field that's empty/null, restore from original before validation
                if (string.IsNullOrEmpty(Fabric.Type))
                    Fabric.Type = originalFabric.Type;
                if (string.IsNullOrEmpty(Fabric.Colour))
                    Fabric.Colour = originalFabric.Colour;
                if (string.IsNullOrEmpty(Fabric.Source))
                    Fabric.Source = originalFabric.Source;
                if (Fabric.isWoven == null)
                    Fabric.isWoven = originalFabric.isWoven;
                if (string.IsNullOrEmpty(Fabric.Description))
                    Fabric.Description = originalFabric.Description;
            }

            // Now validate - but only the fields that were actually posted
            // Clear ModelState completely and rebuild it manually for posted fields only
            var postedKeys = Request.Form.Keys.Where(k => k.StartsWith("Fabric.")).ToList();
            var keysToRemove = ModelState.Keys.Where(k => k.StartsWith("Fabric.") && !postedKeys.Contains(k)).ToList();

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
                await _fabricRepository.UpdateFabricAsync(Fabric.Id, Fabric);
                return RedirectToPage("/Fabrics");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"Error updating fabric: {ex.Message}");
                return Page();
            }
        }
    }
}
