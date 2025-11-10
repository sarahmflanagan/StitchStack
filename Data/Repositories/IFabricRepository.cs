using StitchStack.Models;

namespace StitchStack.Data.Repositories
{
    public interface IFabricRepository
    {
        Task<IEnumerable<Fabric>> GetFabricsAsync();

        Task<Fabric?> GetFabricByIdAsync(int id);

        Task AddFabricAsync(Fabric fabric);

        Task UpdateFabricAsync(int id, Fabric fabric);

        Task DeleteFabricAsync(int id);
    }
}
