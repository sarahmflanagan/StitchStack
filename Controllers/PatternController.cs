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
    public class PatternController : ControllerBase
    {
        private readonly IPatternRepository _patternRepository;
        public PatternController(IPatternRepository patternRepository) {
            _patternRepository = patternRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Pattern>> GetAllPatternsAsync() {
        
            return await _patternRepository.GetPatternsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Pattern>> GetPatternByIdAsync(int id)
        {
            var result = await _patternRepository.GetPatternByIdAsync(id);
            if (result == null) {
                return NotFound();
            }
            return result;
        }

        [HttpPost]
        public async Task AddNewPatternAsync(Pattern pattern)
        {
            await _patternRepository.AddPatternAsync(pattern);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePatternAsync(int id, Pattern pattern)
        {
            if (id == null || pattern == null)
            {
                return BadRequest();
            }
            await _patternRepository.UpdatePatternAsync(id, pattern);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePatternAsync(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            await _patternRepository.DeletePatternAsync(id);
            return NoContent();
        }
    }
}
