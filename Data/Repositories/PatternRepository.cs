using Microsoft.EntityFrameworkCore;
using StitchStack.Data.SqlDB;
using StitchStack.Models;
using System.Linq;

namespace StitchStack.Data.Repositories
{
    public class PatternRepository : IPatternRepository
    {
        private readonly SqlDBContext _dbContext;
        public PatternRepository(SqlDBContext context) {
            _dbContext = context;
        }

        public async Task<IEnumerable<Pattern>> GetPatternsAsync()
        {
            return await _dbContext.Patterns.ToListAsync();
        }

        public async Task<Pattern?> GetPatternByIdAsync(int id)
        {
            var result = await _dbContext.Patterns.FindAsync(id);
            return result ?? null;
        }

        public async Task AddPatternAsync(Pattern pattern)
        {
            await _dbContext.Patterns.AddAsync(pattern);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdatePatternAsync(int id, Pattern pattern)
        {
            var result = await _dbContext.Patterns.FindAsync(id);
            if (result == null) {
                throw new Exception();
            }
            result.Name = pattern.Name;
            result.Description = pattern.Description;
            result.Type = pattern.Type;
            result.Brand = pattern.Brand;
            result.isWoven = pattern.isWoven;
            result.isToilComplete = pattern.isToilComplete;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeletePatternAsync(int id)
        {
            var result = await _dbContext.Patterns.FindAsync(id);
            if (result == null)
            {
                throw new Exception();
            }

            _dbContext.Patterns.Remove(result);
            await _dbContext.SaveChangesAsync();
        }
    }
}
