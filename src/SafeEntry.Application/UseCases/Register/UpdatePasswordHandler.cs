using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Repositories;
using SafeEntry.Domain.Services;
using SafeEntry.Domain.ValueObjects;

namespace SafeEntry.Application.UseCases.UpdatePassword
{
    public class UpdatePasswordHandler
    {
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _hasher;

        public UpdatePasswordHandler(
            IUserRepository userRepo,
            IPasswordHasher hasher)
        {
            _userRepo = userRepo;
            _hasher = hasher;
        }

        public async Task Handle(UpdatePasswordRequest req)
        {
            var user = await _userRepo.GetByEmailAsync(req.Email)
                   ?? throw new ApplicationException("User not found");

            if(req.Password != req.ConfirmPassword)
                throw new ApplicationException("Passwords don't match");

            await _userRepo.ChangePasswordAsync(user.Id, req.Password, _hasher);

            return;
        }
    }
}
