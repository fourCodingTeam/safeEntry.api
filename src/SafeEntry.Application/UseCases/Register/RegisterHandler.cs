﻿using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Response;
using SafeEntry.Domain.Repositories;
using SafeEntry.Domain.Services;
using SafeEntry.Domain.ValueObjects;

namespace SafeEntry.Application.UseCases.Register
{
    public class RegisterHandler
    {
        private readonly IPersonRepository _personRepo;
        private readonly IUserRepository _userRepo;
        private readonly IPasswordHasher _hasher;

        public RegisterHandler(
            IPersonRepository personRepo,
            IUserRepository userRepo,
            IPasswordHasher hasher)
        {
            _personRepo = personRepo;
            _userRepo = userRepo;
            _hasher = hasher;
        }

        public async Task<RegisterResponse> Handle(RegisterRequest req)
        {
            if (await _userRepo.GetByEmailAsync(req.Email) is not null)
                throw new ApplicationException("Email já cadastrado.");

            var person = await _personRepo.GetByIdAsync(req.PersonId);
            if (person == null) throw new ApplicationException("Person Not Found");

            var pwdHash = new PasswordHash(_hasher.Hash(req.Password));
            var user = new User(req.Email, pwdHash, person, req.UserType);
            await _userRepo.AddAsync(user);

            return new RegisterResponse(user.Id, user.Email, person.Id);
        }
    }
}
