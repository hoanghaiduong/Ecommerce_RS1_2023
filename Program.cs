using Ecommerce_2023.Models;
using Ecommerce_2023.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddAutoMapper(typeof(Program));
// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<RoleService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
   
    o.SwaggerDoc("v1", new OpenApiInfo {
        Title="SMART RESTAURANT SERVICE",
        Version = "v3"
    });
 
});
 
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
     
    });
}
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();
