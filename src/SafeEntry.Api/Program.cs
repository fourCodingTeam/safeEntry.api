using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;
using SafeEntry.Domain.Repositories;
using SafeEntry.Domain.Services;
using SafeEntry.Infrastructure.Data;
using SafeEntry.Infrastructure.Repositories;
using SafeEntry.Infrastructure.Security;

using SafeEntry.Application.UseCases.Login;
using SafeEntry.Application.Interfaces;
using SafeEntry.Application.UseCases.Register;
using SafeEntry.Application.UseCases.ListUsers;
using SafeEntry.Application.UseCases.ListResidents;
using SafeEntry.Application.UseCases.Residents;
using SafeEntry.Application.UseCases.Addresses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;

var builder = WebApplication.CreateBuilder(args);

// Registro de repositórios e serviços
builder.Services.AddScoped<IInviteRepository, InviteRepository>();
builder.Services.AddScoped<IInviteService, InviteService>();
builder.Services.AddScoped<IVisitorRespository, VisitorRepository>();

// Registro do LoginHandler e autenticação
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>(); 
builder.Services.AddSingleton<IJwtService, JwtService>(); 
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<ListUsersHandler>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IResidentRepository, ResidentRepository>();
builder.Services.AddScoped<CreateResidentHandler>();
builder.Services.AddScoped<UpdateResidentHandler>();
builder.Services.AddScoped<DeleteResidentHandler>();
builder.Services.AddScoped<ListResidentsHandler>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();

builder.Services.AddScoped<CreateAddressHandler>();
builder.Services.AddScoped<ListAddressesHandler>();
builder.Services.AddScoped<UpdateAddressHandler>();
builder.Services.AddScoped<DeleteAddressHandler>();




var requireAuthPolicy = new AuthorizationPolicyBuilder()
    .RequireAuthenticatedUser()
    .Build();

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new AuthorizeFilter(requireAuthPolicy));
});


// Configuração do PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));

// Configuração do MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDbConnection")));
builder.Services.AddSingleton<MongoDbContext>();

// Adiciona suporte ao Swagger (OpenAPI)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SafeEntry API", Version = "v1" });
});

// Configuração do JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";  // Esquema de autenticação
    options.DefaultChallengeScheme = "Bearer";    // Desafio de autenticação
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;  // Durante o desenvolvimento
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],  // Definido no .env ou appsettings
        ValidAudience = builder.Configuration["Jwt:Audience"],  // Definido no .env ou appsettings
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])  // Sua chave JWT
        ),
    };
});

var app = builder.Build();

// Configura o pipeline de requisição HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SafeEntry API v1");
        c.RoutePrefix = string.Empty; // Swagger será exibido na raiz (http://localhost:5000/)
    });
}

app.UseHttpsRedirection();  // Força redirecionamento de HTTP para HTTPS
app.UseAuthentication();    // Middleware de autenticação
app.UseAuthorization();     // Middleware de autorização

app.MapControllers();       // Mapeia os controllers

app.Run();
