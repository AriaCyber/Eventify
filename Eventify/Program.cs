using Eventify.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddControllers();
var app = builder.Build();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
