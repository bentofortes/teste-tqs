using Microsoft.EntityFrameworkCore;
using backend.Infra;
using backend.Application.DTOs;
using backend.Domain;
using backend.Application.Services;
using backend.Application.Interfaces;
using backend.Infra.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite("Data Source=app.db"));

builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddScoped<ILeadRepository, LeadRepository>();

builder.Services.AddScoped<LeadService>();
builder.Services.AddScoped<TaskService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
