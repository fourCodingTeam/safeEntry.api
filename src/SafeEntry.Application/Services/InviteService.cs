using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Domain.Services;

public class InviteService : IInviteService
{
    private readonly IInviteRepository _inviteRepository;

    public InviteService(IInviteRepository inviteRepository)
    {
        _inviteRepository = inviteRepository;
    }

    public async Task<int> GenerateCodeAsync(int residentId, int vistorId, DateTime startDate, int daysToExpiration, string justification)
    {
        int code;
        do
        {
            code = new Random().Next(1000, 9999);
        }
        while (await _inviteRepository.ExistsCodeForResidentAsync(residentId, code));

        var expiration = startDate.AddDays(daysToExpiration);

        var entity = new Invite(code, residentId, vistorId, startDate, expiration, justification);

        await _inviteRepository.AddAsync(entity);

        return code;
    }

    public async Task<bool> ValidateCodeAsync(int residentId, int vistorId, int code)
    {
        return await _inviteRepository.ValidateCodeAsync(residentId, vistorId, code);
    }
}
