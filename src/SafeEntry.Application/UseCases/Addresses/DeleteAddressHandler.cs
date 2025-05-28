using System.Threading.Tasks;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Addresses
{
    public class DeleteAddressHandler
    {
        private readonly IAddressRepository _repo;
        public DeleteAddressHandler(IAddressRepository repo) => _repo = repo;

        public async Task<bool> Handle(int id)
        {
            var existing = await _repo.GetByIdAsync(id);
            if (existing is null) return false;
            await _repo.DeleteAsync(existing);
            return true;
        }
    }
}
