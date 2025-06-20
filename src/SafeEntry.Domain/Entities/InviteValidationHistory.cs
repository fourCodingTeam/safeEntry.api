﻿namespace SafeEntry.Domain.Entities;

public class InviteValidationHistory
{
    public string? Id { get; protected set; }
    public int AddressId { get; protected set; }
    public int CondominiumId { get; protected set; }
    public string HomeDescription { get; protected set; } = null!;

    public int CreatedByResidentId { get; protected set; }
    public string CreatedByResidentName { get; protected set; } = null!;

    public int VisitorId { get; protected set; }
    public string VisitorName { get; protected set; } = null!;

    public int EmployeeId { get; protected set; }
    public string EmployeeName { get; protected set; } = null!;

    public int Code { get; protected set; }
    public DateTime InviteExpirationDate { get; protected set; }
    public DateTime ValidatedAt { get; protected set; }
    public bool Approval { get; protected set; }

    public InviteValidationHistory(
        int addressId,
        int condominiumId,
        string homeDescription,
        int createdByResidentId,
        string createdByResidentName,
        int visitorId,
        string visitorName,
        int employeeId,
        string employeeName,
        int code,
        DateTime inviteExpirationDate,
        DateTime validatedAt,
        bool approval)
    {
        AddressId = addressId;
        CondominiumId = condominiumId; 
        HomeDescription = homeDescription;
        CreatedByResidentName = createdByResidentName;
        CreatedByResidentId = createdByResidentId;
        VisitorId = visitorId;
        VisitorName = visitorName;
        EmployeeId = employeeId;
        EmployeeName = employeeName;
        Code = code;
        InviteExpirationDate = inviteExpirationDate;
        ValidatedAt = validatedAt;
        Approval = approval;
    }

    public InviteValidationHistory(
       string id,
       int addressId,
       int condominiumId,
       string homeDescription,
       int createdByResidentId,
       string createdByResidentName,
       int visitorId,
       string visitorName,
       int employeeId,
       string employeeName,
       int code,
       DateTime inviteExpirationDate,
       DateTime validatedAt,
       bool approval)
    {
        Id = id;
        AddressId = addressId;
        CondominiumId = condominiumId;
        HomeDescription = homeDescription;
         CreatedByResidentId = createdByResidentId;
        CreatedByResidentName = createdByResidentName;
        VisitorId = visitorId;
        VisitorName = visitorName;
        EmployeeId = employeeId;
        EmployeeName = employeeName;
        Code = code;
        InviteExpirationDate = inviteExpirationDate;
        ValidatedAt = validatedAt;
        Approval = approval;
    }

    protected InviteValidationHistory() { }
}