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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using System.Reflection;
using SafeEntry.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Registro de repositórios e serviços
builder.Services.AddScoped<IInviteRepository, InviteRepository>();
builder.Services.AddScoped<IInviteService, InviteService>();
builder.Services.AddScoped<IVisitorRespository, VisitorRepository>();
builder.Services.AddScoped<IAddressRepository, AddressRepository>();
builder.Services.AddScoped<IAddressService, AddressService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<ICondominiumRepository, CondominiumRepository>();

// Registro de handlers
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPasswordHasher, BCryptPasswordHasher>();
builder.Services.AddSingleton<IJwtService, JwtService>();
builder.Services.AddScoped<LoginHandler>();
builder.Services.AddScoped<RegisterHandler>();
builder.Services.AddScoped<ListUsersHandler>();
builder.Services.AddScoped<ListResidentsByAddressIdHandler>();
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<CreateResidentHandler>();
builder.Services.AddScoped<UpdateResidentHandler>();
builder.Services.AddScoped<DeleteResidentHandler>();
builder.Services.AddScoped<ListResidentsHandler>();
builder.Services.AddScoped<IResidentRespository, ResidentRepository>();

// REMOVE a exigência global de autenticação (temporariamente)
builder.Services.AddControllers(); // ← libera todas as rotas

// Configuração do PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("SupabaseConnection")));

// Configuração do MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
    new MongoClient(builder.Configuration.GetConnectionString("MongoDbConnection")));
builder.Services.AddSingleton<MongoDbContext>();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SafeEntry API", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        Description = "Informe ‘Bearer {token}’"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Configuração do JWT (mantido, mas você pode desabilitar esse bloco se quiser)
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "Bearer";
    options.DefaultChallengeScheme = "Bearer";
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(
            System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
        ),
    };
});

var app = builder.Build();

// Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SafeEntry API v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

// Comentado para ignorar autenticação
// app.UseAuthentication();
// app.UseAuthorization();

app.MapControllers();

app.Run();
