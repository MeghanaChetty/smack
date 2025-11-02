using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using smack.application;
using smack.core.Interfaces;
using smack.infrastructure.Data;
using smack.infrastructure.Repositories;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;
using smack.application.Validators;
using smack.application.DTOs.Restaurant;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Retrieving this: "SmackDatabase"
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

// Register FluentValidation separately (new way)
builder.Services.AddValidatorsFromAssemblyContaining<MappingProfile>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateRestaurantValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<RestaurantDto>();
var connectionString =
    builder.Configuration.GetConnectionString("SmackDatabase")
        
        ?? throw new InvalidOperationException("Connection string" + "'DefaultConnection' not found.");

builder.Services.AddDbContext<SmackDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(); 
    // Uncomment this for Scalar UI
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers(); // Only map controllers once, here

app.Run();
