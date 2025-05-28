using System.Threading.Tasks;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Residents
{
    public class DeleteResidentHandler
    {
        private readonly IResidentRepository _repo;
        public DeleteResidentHandler(IResidentRepository repo) => _repo = repo;

        public async Task<bool> Handle(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;
            await _repo.DeleteAsync(id);
            return true;
        }
    }
}
