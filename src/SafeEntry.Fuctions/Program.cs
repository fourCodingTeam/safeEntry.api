using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using SafeEntry.Application.Interfaces;
using SafeEntry.Application.Services;
using SafeEntry.Application.UseCases.ListResidents;
using SafeEntry.Application.UseCases.Register;
using SafeEntry.Domain.Repositories;
using SafeEntry.Domain.Services;
using SafeEntry.Infrastructure.Data;
using SafeEntry.Infrastructure.Repositories;
using SafeEntry.Infrastructure.Security;

var builder = FunctionsApplication.CreateBuilder(args);

builder.Services.AddScoped<IInviteService, InviteService>();
builder.Services.AddScoped<IInviteRepository, InviteRepository>();
builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IInviteValidationHistoryService, InviteValidationHistoryService>();
builder.Services.AddScoped<IInviteValidationHistoryRepository, InviteValidationHistoryRepository>();
builder.Services.AddScoped<ICondominiumRepository, CondominiumRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddScoped<ListResidentsByIdHandler>();
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();

builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDbConnection")));
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));

builder.ConfigureFunctionsWebApplication();

builder.Build().Run();
