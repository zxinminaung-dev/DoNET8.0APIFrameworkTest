using ChatApp.Web.DataAccess;
using ChatApp.Web.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

string? connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<DatabaseContext>(options =>
options.UseSqlServer(connString));
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddRepositories(Assembly.GetExecutingAssembly());
builder.Services.AddServices(Assembly.GetExecutingAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
