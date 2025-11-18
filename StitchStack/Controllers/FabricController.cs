using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using StitchStack.Data.Repositories;
using StitchStack.Models;
using System.Threading.Tasks;

namespace StitchStack.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FabricController : ControllerBase
    {
        private readonly IFabricRepository _fabricRepository;
        public FabricController(IFabricRepository fabricRepository)
        {
            _fabricRepository = fabricRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Fabric>> GetAllFabricsAsync()
        {

            return await _fabricRepository.GetFabricsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Fabric>> GetFabricByIdAsync(int id)
        {
            var result = await _fabricRepository.GetFabricByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task AddNewFabricAsync(Fabric Fabric)
        {
            await _fabricRepository.AddFabricAsync(Fabric);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFabricAsync(int id, Fabric Fabric)
        {
            if (id == null || Fabric == null)
            {
                return BadRequest();
            }
            await _fabricRepository.UpdateFabricAsync(id, Fabric);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFabricAsync(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            await _fabricRepository.DeleteFabricAsync(id);
            return NoContent();
        }
    }
}
