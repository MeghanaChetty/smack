using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using smack.core.Interfaces;
using smack.infrastructure.Data;
using smack.infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Retrieving this: "SmackDatabase"
var connectionString =
    builder.Configuration.GetConnectionString("SmackDatabase")
        
        ?? throw new InvalidOperationException("Connection string" + "'DefaultConnection' not found.");

builder.Services.AddDbContext<SmackDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); // Uncomment this for Scalar UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Only map controllers once, here

app.Run();
