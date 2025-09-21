using Microsoft.EntityFrameworkCore;
using timesheets.Infrastructure.Data;
using timesheets.Infrastructure.Repositories;
using timesheets.Domain.Interfaces;
using timesheets.Application.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add AutoMapper
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

// Add Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() {
        Title = "Timesheets API",
        Version = "v1",
        Description = "A simple timesheet management API"
    });
});

// Add Entity Framework
builder.Services.AddDbContext<TimesheetDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register repositories
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();

// Register mappers
builder.Services.AddScoped<timesheets.Application.Mappers.TimesheetMapper>();
builder.Services.AddScoped<timesheets.Application.Mappers.ProjectMapper>();

// Register services (keeping them for backward compatibility if needed)
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITimesheetService, TimesheetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// Enable Swagger in all environments for easier testing
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Timesheets API v1");
    c.RoutePrefix = string.Empty; // Set Swagger UI at the app's root
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Make the implicit Program class public for integration tests
public partial class Program { }