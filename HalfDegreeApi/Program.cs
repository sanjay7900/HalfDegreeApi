using HalfDegreeApi.Controllers;
using HalfDegreeApi.Data;
using HalfDegreeApi.LoggingExcep;
using HalfDegreeApi.Models;
using HalfDegreeApi.Repositories;
using HalfDegreeApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", true, true);
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration/*configuration.GetSection("Serilog.WriteTo.path")*/)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:AudienceWebAPI"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]))
    };
});


//var logr = new Serilog.LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logr);

// Add services to the container.
builder.Services.AddDbContext<HalfDegreeApiDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("Connection") ?? throw new InvalidOperationException(" Connection String Not Found")));
builder.Services.AddTransient<IUser, UserData>();
builder.Services.AddTransient<IProduct<Product>, ProductData>();
builder.Services.AddTransient<IOrder<Order>, OrderData>();
builder.Services.AddTransient<IRole<Roles>, RolesData>();

builder.Services.AddControllers();
builder.Services.AddCors(option => option.AddPolicy("SpacificPolicy", builder => builder.WithOrigins("https://localhost:7167").AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddAutoMapper(typeof(Program));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Authorization using bearer token",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
//var logr = new Serilog.LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).Enrich.FromLogContext().CreateLogger();
//builder.Logging.ClearProviders();
//builder.Logging.AddSerilog(logr);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ApplyCustomException();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAuthentication();

app.UseAuthorization();
app.UseCors("SpacificPolicy");

app.MapControllers();

app.Run();
