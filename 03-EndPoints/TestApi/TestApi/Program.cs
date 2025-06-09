using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Text.Json.Serialization;
using TestApi.test;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Service;
using Travel.Domain.Services.AppService;
using Travel.EndPoint.TravelJob;
using Travel.InfraStructure.EfCore.Common;
using Travel.InfraStructure.EfCore.Repositories;

var builder = WebApplication.CreateBuilder(args);


var connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connection));


builder.Services.AddScoped<ICheckListRepository, CheckListRepository>();
builder.Services.AddScoped<ITripRepository, TripRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICheckListTripRepository, CheckListTripRepository>();

builder.Services.AddScoped<ICheckListService, CheckListService>();
builder.Services.AddScoped<ITripService, TripService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICheckListTripService, CheckListTripService>();

builder.Services.AddScoped<ICheckListAppService, CheckListAppService>();
builder.Services.AddScoped<ITripAppService, TripAppService>();
builder.Services.AddScoped<IUserAppService, UserAppService>();
builder.Services.AddScoped<ICheckListTripAppService, CheckListTripAppService>();


builder.Services.AddScoped<StartTripJob>();
builder.Services.AddScoped<EndTripJob>();
builder.Services.AddScoped<ITripJobScheduler, TripJobScheduler>();

builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();


builder.Services.AddQuartzHostedService(q => 
{
    q.WaitForJobsToComplete = true;

});


builder.Services.AddControllers().AddJsonOptions(opt =>
{
    opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();