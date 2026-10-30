using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using STM.API.Extensions;
using STM.Application.Interfaces.Repositories;
using STM.Application.Interfaces.Services;
using STM.Application.Services;
using STM.Infrastructure.Persistence;
using STM.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IProjectService, ProjectService>();

//DB Context
builder.Services.AddDbContext<STMDBContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityServices();
builder.Services.AddScoped<IAuthService, AuthService>();

// JWT TokenService + JWT Bearer authentication
builder.Services.AddJwtAuthentication(builder.Configuration);


var app = builder.Build();

app.MapOpenApi();
app.MapScalarApiReference(options =>
{
    options.WithTitle("Smart Task Management API");
});

//if (app.Environment.IsDevelopment())
//{
//    app.MapOpenApi();
//    app.MapScalarApiReference();
//}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/health", () =>
{
    return Results.Ok(new
    {
        Status = "Healthy",
        Application = "STM.API",
        Timestamp = DateTime.UtcNow
    });
});

app.Run();