using SafeEntry.Application.Interfaces;
using SafeEntry.Application.UseCases.ListResidents;
using SafeEntry.Contracts.Request;
using SafeEntry.Domain.Entities;
using SafeEntry.Domain.Repositories;
using SafeEntry.Domain.Services;

namespace SafeEntry.Application.Services;

public class InviteValidationHistoryService : IInviteValidationHistoryService
{
    private readonly IInviteValidationHistoryRepository _historyRepository;
    private readonly IInviteRepository _inviteRepository;
    private readonly ListResidentsByIdHandler _listResidentsByIdHandler;
    private readonly IAddressService _addressService;
    private readonly IEmployeeService _employeeService;

    public InviteValidationHistoryService(
        IInviteRepository inviteRepository, 
        ListResidentsByIdHandler listResidentsByIdHandler, 
        IVisitorRepository visitorRepository, 
        IAddressService addressService, 
        IInviteValidationHistoryRepository historyRepository,
        IEmployeeService employeeService)
    {
        _inviteRepository = inviteRepository;
        _listResidentsByIdHandler = listResidentsByIdHandler;
        _addressService = addressService;
        _historyRepository = historyRepository;
        _employeeService = employeeService;
    }

    public async Task AddAsync(InviteHistoryRequest request)
    {
        var addressId = request.AddressId;
        var visitorId = request.VisitorId;
        var code = request.Code;
        var validatedAt = request.DateNow;

        var invite = await _inviteRepository.GetInviteByAddressIdAndVisitorIdAsync(addressId, visitorId, code); 

        var address = await _addressService.GetByResidentIdAsync(invite.ResidentId);

        var homeDescription = !string.IsNullOrWhiteSpace(address.HomeStreet)
            ? $"{address.HomeStreet}, casa número {address.HomeNumber}"
            : $"Número {address.HomeNumber}";

        var resident = await _listResidentsByIdHandler.Handle(invite.ResidentId);

        var employee = await _employeeService.GetEmployeeByIdAsync(request.EmployeeId);

        var entity = new InviteValidationHistory(
            addressId, 
            address.CondominiumId, 
            homeDescription, 
            resident.Name, 
            visitorId, 
            invite.VisitorName,
            employee.Id,
            employee.Name,
            code,
            invite.ExpirationDate,
            request.DateNow,
            request.Approval);

        await _historyRepository.AddAsync(entity);

        return;
    }

    public async Task<IEnumerable<InviteValidationHistory>> GetAllAsync(int condominiumId)
    {
        var invites = await _historyRepository.GetAllAsync(condominiumId);

        if(!invites.Any())
            throw new Exception("Invite not found");

        return invites;
    }

    public async Task<IEnumerable<InviteValidationHistory>> GetLastSevenDaysAsync(int condominiumId)
    {
        var invites = await _historyRepository.GetLastSevenDaysAsync(condominiumId);

        if (!invites.Any())
            throw new Exception("Invite not found");

        return invites;
    }

    public async Task<InviteValidationHistory> GetInviteHistorybyIdAsync(string id)
    {
        var inviteHistory = await _historyRepository.GetInviteHistorybyIdAsync(id);

        if(inviteHistory == null)
            throw new Exception("Invite not found");

        return inviteHistory;
    }
}