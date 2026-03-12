using HarranKampusAsistani.API.Data;
using HarranKampusAsistani.API.Middleware;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();

// app.UseMiddleware<ApiKeyMiddleware>();

app.MapControllers();

app.Run("http://0.0.0.0:8080");