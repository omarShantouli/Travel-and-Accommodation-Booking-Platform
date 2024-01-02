using Application;
using AutoMapper;
using Cqrs.Hosts;
using Domain.Entities;
using FluentAssertions.Common;
using Infrastructure.Data;
using Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using static Domain.Interfaces.IRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//builder.Services.AddTransient<IEmailService, EmailService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IRepository<City>, CityRepository>();
builder.Services.AddScoped<IRepository<Hotels>, HotelsRepository>();
builder.Services.AddScoped<IRepository<Rooms>, RoomsRepository>();
builder.Services.AddScoped<IRepository<Images>, ImagesRepository>();
builder.Services.AddScoped<IRepository<Bookings>, BookingsRepository>();
builder.Services.AddScoped<IRepository<Reviews>, ReviewsRepository>();


builder.Services.AddApplication();

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
