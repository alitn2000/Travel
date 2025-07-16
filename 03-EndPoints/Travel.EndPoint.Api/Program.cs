using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
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
using Travel.Domain.Core.Events;
using Travel.Domain.Service.Features.Commands.CheckLists.AddCheckList;
using Travel.Domain.Service.Features.Commands.CheckLists.GetCheckListById;
using Travel.Domain.Service.Features.Commands.CheckLists.UpdateCheckList;
using Travel.Domain.Service.Features.Commands.CheckListTrips.AddCheckListTrip;
using Travel.Domain.Service.Features.Commands.CheckListTrips.UpdateIsChecked;
using Travel.Domain.Service.Features.Commands.Profiles.UpdateProfile;
using Travel.Domain.Service.Features.Commands.Trips.AddTrip;
using Travel.Domain.Service.Features.Commands.Trips.AddUsersToTrip;
using Travel.Domain.Service.Features.Commands.Trips.UpdateTrip;
using Travel.Domain.Service.Features.Commands.Users.GenerateToken;
using Travel.Domain.Service.Features.Commands.Users.GetToken;
using Travel.Domain.Service.Features.Commands.Users.Login;
using Travel.Domain.Service.Features.Queries.Checklists.CheckCheckListExist;
using Travel.Domain.Service.Features.Queries.Checklists.GetAllCheckListsAsync;
using Travel.Domain.Service.Features.Queries.CheckListTrips.GetAllCheckListTrips;
using Travel.Domain.Service.Features.Queries.CheckListTrips.GetAllIsCheckedLists;
using Travel.Domain.Service.Features.Queries.Trips.CheckUsersHaveTripById;
using Travel.Domain.Service.Features.Queries.Trips.GetUsersTripsById;
using Travel.Domain.Service.Features.Queries.Users.CheckUserExistById;
using Travel.Domain.Service.NewFolder;
using Travel.EndPoint.Api.Jobs;
using Travel.EndPoint.Api.Middlewares;
using Travel.EndPoint.Api.Models.UserModels;
using Travel.EndPoint.Api.Validations;
using Travel.EndPoints.Jobs.TripJobs;
using Travel.InfraStructure.EfCore.Common;
using Travel.InfraStructure.EfCore.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// repository
builder.Services.AddScoped<ICheckListRepository, CheckListRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICheckListTripRepository, CheckListTripRepository>();
builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<IUserTripRepository, UserTripRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//services
//builder.Services.AddScoped<ICheckListService, CheckListService>();
//builder.Services.AddScoped<ITripService, TripService>();
//builder.Services.AddScoped<IUserService, UserService>();
//builder.Services.AddScoped<ICheckListTripService, CheckListTripService>();
builder.Services.AddScoped<IOTPService, OTPService>();
//builder.Services.AddScoped<IEmailService, EmailService>();
//builder.Services.AddScoped<IProfileService, ProfileService>();
// appservices
//builder.Services.AddScoped<ICheckListAppService, CheckListAppService>();
//builder.Services.AddScoped<ITripAppService, TripAppService>();
//builder.Services.AddScoped<IUserAppService, UserAppService>();
//builder.Services.AddScoped<ICheckListTripAppService, CheckListTripAppService>();
//builder.Services.AddScoped<IProfileAppService, ProfileAppService>();
// jobs
builder.Services.AddScoped<StartTripJob>();
builder.Services.AddScoped<EndTripJob>();
builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddScoped<ITripJobScheduler,TripJobScheduler>();
builder.Services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);
builder.Services.AddHostedService<TripRecoveryService>();

// cache
builder.Services.AddMemoryCache();
// httpcontext
builder.Services.AddHttpContextAccessor();

// fluent validation
builder.Services.AddScoped<IValidator<UserLoginModel>, UserLoginModelValidator>();


//mediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(LoginCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetTokenCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GenerateTokenCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CheckUserExistByIdQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AddTripCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetUsersTripsByIdQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAllCheckListsAsyncQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AddCheckListCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateCheckListCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AddCheckListTripCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CheckUsersHaveTripByIdQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(AddUsersToTripCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateTripCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateProfileCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(UpdateIsCheckedCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAllCheckListTripsQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAllIsCheckedListsQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetCheckListByIdCommand).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(CheckCheckListExistQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(GetAllCheckListsAsyncQuery).Assembly);
    cfg.RegisterServicesFromAssembly(typeof(RegisterUserEvent).Assembly);
});

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

app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthentication();   //baraye jwt

app.UseAuthorization();

app.MapControllers();

app.Run();
