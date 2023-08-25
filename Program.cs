using Ecommerce_2023.Models.Interfaces;
using Ecommerce_2023.Models.Role;
using Ecommerce_2023.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<RoleContext>(options=>options.UseSqlServer(builder.Configuration.GetConnectionString("connectionStr_ecommerce_rs1")));

builder.Services.AddControllers();
builder.Services.AddScoped<RoleService>();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
