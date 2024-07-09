using FluentValidation;
using Microsoft.EntityFrameworkCore;
using NightClub.DataAccess;
using NightClub.Interfaces;
using NightClub.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Context
var connectionString = builder.Configuration.GetConnectionString("NightClubDB");
builder.Services.AddDbContext<NightClubContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddScoped<IValidator<Member>, MemberValidator>();
builder.Services.AddScoped<IMemberService, MemberService>();


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
