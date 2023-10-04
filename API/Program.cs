using API.Contracts;
using API.Data;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

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

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
