using ClementoniWebAPI.Models.DB;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<FormazioneDBContext>(options => options.UseSqlServer("Data Source=AC-RCATALDI1\\SQLEXPRESS;Initial Catalog=Formazione DB;Integrated Security=True;TrustServerCertificate=true;"));
builder.Services.AddMediatR(Configuration =>
Configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
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
