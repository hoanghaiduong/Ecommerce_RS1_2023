using Ecommerce_2023.Models;
using Ecommerce_2023.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.EntityFrameworkCore;
using FirebaseAdminAuthentication.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using FirebaseAdmin.Auth;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;

builder.Services.AddSingleton(env);
void configFirebase(WebApplicationBuilder builder,ConfigurationManager configuration)
{
    var firebaseConfigJson = configuration.GetValue<string>("FIREBASE_CONFIG");
    var firebaseFileConfig = configuration.GetValue<string>("FIREBASE_CONFIG_FILE");
    var firebaseApp = FirebaseApp.Create(new AppOptions()
    {
        Credential = GoogleCredential.FromFile(firebaseFileConfig),
    });

    // Retrieve services by passing the defaultApp variable...
    var defaultAuth = FirebaseAuth.GetAuth(firebaseApp);
    defaultAuth = FirebaseAuth.DefaultInstance;
    builder.Services.AddSingleton(firebaseApp);
    builder.Services.AddSingleton(defaultAuth);
    builder.Services.AddFirebaseAuthentication();
};
void configDatabase(WebApplicationBuilder builder, ConfigurationManager configuration)
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    builder.Services.AddDbContext<DatabaseContext>(o => o.UseSqlServer(connectionString));
};
configFirebase(builder,builder.Configuration);
configDatabase(builder,builder.Configuration);



void addServices(WebApplicationBuilder builder)
{
    builder.Services.AddControllers();
    builder.Services.AddScoped<RoleService>();
    builder.Services.AddScoped<UserService>();
};
addServices(builder);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
   
    o.SwaggerDoc("v1", new OpenApiInfo {
        Title="SMART RESTAURANT SERVICE",
        Version = "v3"
    });
    o.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
      
        Scheme = "Bearer"
    });
    o.AddSecurityRequirement(new OpenApiSecurityRequirement()
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            },
            Scheme = "Bearer",
            Name = "Bearer",
            In = ParameterLocation.Header,
            
        },
        new List<string>()
    }
});


});
builder.Services.AddAuthorization();

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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
            Path.Combine(env.ContentRootPath, "uploads")),
    RequestPath = "/uploads"
});
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
//app.UseWelcomePage();
app.Run();
