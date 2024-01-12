using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.IO;
using System;
using User_Management_Application.Database;
using User_Management_Application.Models;
using User_Management_Application.Repositroy;
using User_Management_Application.Repositroy.IRepository;
using User_Management_Application.Service;

var builder = WebApplication.CreateBuilder(args);

// Directly use the connection string from the configuration
var _connectionString = builder.Configuration.GetConnectionString("Context");
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer(_connectionString));
builder.Services.AddTransient(typeof(UserRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient(typeof(UserService), typeof(UserService));
builder.Services.Configure<AppConfig>(builder.Configuration.GetSection("AppConfig"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swagger =>
{
    swagger.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "UMA API v1\r\n",
        Description = "User Management Application Api",
        Contact = new OpenApiContact
        {
            Name = "Mercy Ogbenjuwa",
            Email = "mercy.ogbenjuwa@gmail.com"
        }
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    swagger.IncludeXmlComments(xmlPath);
});

var AllowSpecificOrigins = "_myAllowSpecificOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins,
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                    });
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(AllowSpecificOrigins);
app.UseAuthorization();
app.MapControllers();
app.Run();
