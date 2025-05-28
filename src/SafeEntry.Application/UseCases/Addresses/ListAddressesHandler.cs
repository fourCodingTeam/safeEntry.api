using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafeEntry.Contracts.Responses;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.Addresses
{
    public class ListAddressesHandler
    {
        private readonly IAddressRepository _repo;
        public ListAddressesHandler(IAddressRepository repo) => _repo = repo;

        public async Task<IEnumerable<AddressResponse>> Handle()
        {
            var addrs = await _repo.GetAllAsync();
            return addrs.Select(a =>
                new AddressResponse(
                    a.Id, a.Street, a.Number,
                    a.Neighborhood, a.ZipCode,
                    a.City, a.State, a.Country
                )
            );
        }
    }
}
