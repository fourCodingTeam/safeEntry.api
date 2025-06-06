﻿using SafeEntry.Domain.Services;

namespace SafeEntry.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
    Task ChangePasswordAsync(Guid userId, string newPassword, IPasswordHasher passwordHasher);
}
