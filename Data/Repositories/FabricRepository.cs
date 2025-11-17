using Microsoft.EntityFrameworkCore;
using StitchStack.Data.SqlDB;
using StitchStack.Models;
using System.Linq;

namespace StitchStack.Data.Repositories
{
    public class FabricRepository : IFabricRepository
    {
        private readonly SqlDBContext _dbContext;
        public FabricRepository(SqlDBContext context)
        {
            _dbContext = context;
        }

        public async Task<IEnumerable<Fabric>> GetFabricsAsync()
        {
            return await _dbContext.Fabrics.ToListAsync();
        }

        public async Task<Fabric?> GetFabricByIdAsync(int id)
        {
            var result = await _dbContext.Fabrics.FindAsync(id);
            return result ?? null;
        }

        public async Task AddFabricAsync(Fabric fabric)
        {
            await _dbContext.Fabrics.AddAsync(fabric);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateFabricAsync(int id, Fabric fabric)
        {
            var result = await _dbContext.Fabrics.FindAsync(id);
            if (result == null)
            {
                throw new Exception();
            }
            result.Type = fabric.Type;
            result.Description = fabric.Description;
            result.Colour = fabric.Colour;
            result.Source = fabric.Source;
            result.isWoven = fabric.isWoven;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFabricAsync(int id)
        {
            var result = await _dbContext.Fabrics.FindAsync(id);
            if (result == null)
            {
                throw new Exception();
            }

            _dbContext.Fabrics.Remove(result);
            await _dbContext.SaveChangesAsync();
        }
    }
}
