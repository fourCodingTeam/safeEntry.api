using SafeEntry.Contracts.Response;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Application.UseCases.ListUsers
{
    public class ListUsersHandler
    {
        private readonly IUserRepository _repo;
        public ListUsersHandler(IUserRepository repo)
            => _repo = repo;

        public async Task<IEnumerable<UserResponse>> Handle()
        {
            var users = await _repo.GetAllAsync();
            return users.Select(u =>
                new UserResponse(u.Id, u.Email, u.PersonId)
            );
        }
    }
}
