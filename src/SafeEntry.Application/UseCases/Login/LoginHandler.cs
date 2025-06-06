namespace SafeEntry.Application.UseCases.Login;

using System;
using System.Threading.Tasks;
using SafeEntry.Contracts.Request;
using SafeEntry.Contracts.Response;
using SafeEntry.Domain.Services;           
using SafeEntry.Application.Interfaces;
using SafeEntry.Domain.Repositories;

public class LoginHandler
{
    private readonly IUserRepository _repo;
    private readonly IPasswordHasher _hasher;
    private readonly IJwtService _jwt;

    public LoginHandler(
        IUserRepository repo,
        IPasswordHasher hasher,
        IJwtService jwt)
    {
        _repo = repo;
        _hasher = hasher;
        _jwt = jwt;
    }

    public async Task<LoginResponse> Handle(LoginRequest request)
    {
        var user = await _repo.GetByEmailAsync(request.Email)
                   ?? throw new ApplicationException("Usuário não encontrado");

        if (!user.ValidatePassword(request.Password, _hasher))
            throw new ApplicationException("Credenciais inválidas");

        var token = _jwt.GenerateToken(user.Id, request.Email);

        if (user.IsFirstLogin)
            return new LoginResponse(token, _jwt.GetExpiration(), true, user.PersonId, user.Person.Name, user.UserType);

        return new LoginResponse(token, _jwt.GetExpiration(), false, user.PersonId, user.Person.Name, user.UserType);
    }
}
