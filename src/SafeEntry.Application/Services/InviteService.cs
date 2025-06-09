using SafeEntry.Application.Interfaces;
using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;

namespace SafeEntry.Domain.Services;

public class InviteService : IInviteService
{
    private readonly IInviteRepository _inviteRepository;
    private readonly IVisitorRepository _visitorRepository;
    private readonly IAddressRepository _addressRepository;
    private readonly IInviteValidationHistoryService _historyService;

    public InviteService(IInviteRepository inviteRepository, IVisitorRepository visitorRepository, IAddressRepository addressRepository, IInviteValidationHistoryService historyService )
    {
        _inviteRepository = inviteRepository;
        _visitorRepository = visitorRepository;
        _addressRepository = addressRepository;
        _historyService = historyService;
    }

    public async Task<int> GenerateCodeAsync(GenerateInviteRequest request)
    {
        var residentId = request.ResidentId;
        var visitorName = request.VisitorName;
        var visitorPhoneNumber = request.VisitorPhoneNumber;
        var startDate = request.StartDate;
        var daysToExpiration = request.DaysToExpiration;
        var justification = request.Justification;

        var address = await _addressRepository.GetByResidentIdAsync(residentId);

        if (address == null)
            throw new Exception("Address not found");

        var addressId = address.Id;

        int code;
        do
        {
            code = new Random().Next(1000, 9999);
        }
        while (await _inviteRepository.ExistsCodeForAddressAsync(addressId, code));

        var visitor = await _visitorRepository.GetOrCreateAsync(visitorName, visitorPhoneNumber);

        var expiration = startDate.AddDays(daysToExpiration);

        var entity = new Invite(code, residentId, addressId, visitor.Id, visitor.Name, startDate, expiration, daysToExpiration, justification, true);

        await _inviteRepository.AddAsync(entity);

        return code;
    }

    public async Task<bool> ValidateCodeAsync(ValidateInviteRequest request)
    {
        var addressId = request.AddressId;
        var visitorId = request.VisitorId;
        var employeeId = request.EmployeeId;
        var code = request.Code;
        var dateNow = request.DateNow;

        var approval = await _inviteRepository.ValidateCodeAsync(addressId, visitorId, code, dateNow);

        var history = new InviteHistoryRequest(
            addressId,
            visitorId,
            employeeId,
            code,
            dateNow,
            approval);

        await _historyService.AddAsync(history);

        return approval;
    }

    public async Task<Invite> GetInviteByResidentIdAndVisitorIdAsync(int residentId, int visitorId, int code)
    {
        return await _inviteRepository.GetInviteByResidentIdAndVisitorIdAsync(residentId, visitorId, code);
    }

    public async Task<Invite> GetInviteByAddressIdAndVisitorIdAsync(int addressId, int visitorId, int code)
    {
        return await _inviteRepository.GetInviteByAddressIdAndVisitorIdAsync(addressId, visitorId, code);
    }

    public async Task<IEnumerable<Invite>> GetInvitesByAddressIdAsync(int addressId)
    {
        return await _inviteRepository.GetInvitesByAddressIdAsync(addressId);
    }

    public async Task<IEnumerable<Invite>> GetInvitesByResidentIdAsync(int residentId)
    {
        return await _inviteRepository.GetInvitesByResidentIdAsync(residentId);
    }
    public async Task<long> CountInvitesByAddressIdAsync(int addressId)
    {
        return await _inviteRepository.CountByAddressIdAsync(addressId);
    }
    public async Task<bool> ActivateInviteAsync(int addressId, int visitorId, int code)
    {
        return await _inviteRepository.ActivateInviteAsync(addressId, visitorId, code);
    }

    public async Task<bool> DeactivateInviteAsync(int residentId, int visitorId, int code)
    {
        return await _inviteRepository.DeactivateInviteAsync(residentId, visitorId, code);
    }
}
