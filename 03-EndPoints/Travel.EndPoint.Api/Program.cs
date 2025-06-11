using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Text;
using System.Text.Json.Serialization;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Service;
using Travel.Domain.Services.AppService;
using Travel.EndPoint.Api.Jobs;
using Travel.EndPoint.Api.Models.UserModels;
using Travel.EndPoints.Jobs.TripJobs;
using Travel.InfraStructure.EfCore.Common;
using Travel.InfraStructure.EfCore.Repositories;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// repository
builder.Services.AddScoped<ICheckListRepository, CheckListRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICheckListTripRepository, CheckListTripRepository>();
//services
builder.Services.AddScoped<ICheckListService, CheckListService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICheckListTripService, CheckListTripService>();
// appservices
builder.Services.AddScoped<ICheckListAppService, CheckListAppService>();
builder.Services.AddScoped<ITripAppService, TripAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<ICheckListTripAppService, CheckListTripAppService>();
// jobs
builder.Services.AddScoped<Travel.EndPoints.Jobs.TripJobs.StartTripJob>();
builder.Services.AddScoped<Travel.EndPoints.Jobs.TripJobs.EndTripJob>();
builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddScoped<ITripJobScheduler,TripJobScheduler>();
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
builder.Services.AddHostedService<TripRecoveryService>();


// fluent validation

builder.Services.AddControllers()
    .AddJsonOptions(op =>
    {
        op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],

        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],

        ValidateLifetime = true,

        ClockSkew = TimeSpan.Zero,
    };
});                                                                                                                ///////////  JWT Authentication
builder.Services.AddSwaggerGen(conf =>
{
    conf.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Travel API",
        Version = "v1",
        Description = "API for managing travel trips and related services."
    });                       

    conf.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",                                                                                                        
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\r\nExample: Bearer eyJhbGciOiJIUzI1NiIs..."
    });

    conf.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
var Connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Connection));                                                 // SQL SERVER
var app = builder.Build();

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();   //baraye jwt

app.UseAuthorization();

app.MapControllers();

app.Run();
