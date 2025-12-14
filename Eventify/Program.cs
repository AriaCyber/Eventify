using Eventify.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// CORS
builder.Services.AddCors(o => o.AddPolicy("AcceptAll",
    p => p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();

var app = builder.Build();

// app.UseHttpsRedirection();   
app.UseCors("AcceptAll");

app.MapControllers();
app.Run();
