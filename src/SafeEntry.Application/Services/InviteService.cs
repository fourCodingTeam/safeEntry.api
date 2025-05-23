using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Domain.Services;

public class InviteService : IInviteService
{
    private readonly IInviteRepository _inviteRepository;
    private readonly IVisitorRespository _visitorRepository;

    public InviteService(IInviteRepository inviteRepository, IVisitorRespository visitorRespository)
    {
        _inviteRepository = inviteRepository;
        _visitorRepository = visitorRespository;
    }

    public async Task<int> GenerateCodeAsync(GenerateInviteRequest request)
    {
        var residentId = request.ResidentId;
        var visitorName = request.VisitorName;
        var visitorPhoneNumber = request.VisitorPhoneNumber;
        var startDate = request.StartDate;
        var daysToExpiration = request.DaysToExpiration;
        var justification = request.Justification;

        int code;
        do
        {
            code = new Random().Next(1000, 9999);
        }
        while (await _inviteRepository.ExistsCodeForResidentAsync(residentId, code));

        var visitor = _visitorRepository.GetOrCreateAsync(visitorName, visitorPhoneNumber);

        var expiration = startDate.AddDays(daysToExpiration);

        var entity = new Invite(code, residentId, visitor.Id, startDate, expiration, justification);

        await _inviteRepository.AddAsync(entity);

        return code;
    }

    public async Task<bool> ValidateCodeAsync(ValidateInviteRequest request)
    {
        var residentId = request.ResidentId;
        var vistorId = request.VisitorId;
        var code = request.Code;

        return await _inviteRepository.ValidateCodeAsync(residentId, vistorId, code);
    }

    public async Task<Invite> GetInviteByResidentIdAndVisitorIdAsync(ValidateInviteRequest request)
    {
        var residentId = request.ResidentId;
        var vistorId = request.VisitorId;
        var code = request.Code;

        return await _inviteRepository.GetInviteByResidentIdAndVisitorIdAsync(residentId, vistorId, code);
    }

    public async Task<IEnumerable<Invite>> GetInvitesByResidentIdAsync(int residentId)
    {
        return await _inviteRepository.GetInvitesByResidentIdAsync(residentId);
    }
}
