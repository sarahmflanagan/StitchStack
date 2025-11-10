using StitchStack.Models;

namespace StitchStack.Data.Repositories
{
    public interface IPatternRepository
    {
        Task<IEnumerable<Pattern>> GetPatternsAsync();

        Task<Pattern?> GetPatternByIdAsync(int id);

        Task AddPatternAsync(Pattern pattern);

        Task UpdatePatternAsync(int id, Pattern pattern);

        Task DeletePatternAsync(int id);
    }
}
