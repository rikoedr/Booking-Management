using API.Contracts;
using API.Data;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingManagementDbContext>(option => option.UseSqlServer(connectionString));

// Add repositories to the container.
builder.Services.AddScoped<AccountRepository, AccountRepository>();
builder.Services.AddScoped<AccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<BookingRepository, BookingRepository>();
builder.Services.AddScoped<EducationRepository, EducationRepository>();
builder.Services.AddScoped<EmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<RoleRepository, RoleRepository>();
builder.Services.AddScoped<RoomRepository, RoomRepository>();
builder.Services.AddScoped<UniversityRepository, UniversityRepository>();

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            var errors = context.ModelState.Values
            .SelectMany(v => v.Errors)
            .Select(v => v.ErrorMessage);

            return new BadRequestObjectResult(new ResponseValidatorHandler(errors));
        };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add FluentValidation Services
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
