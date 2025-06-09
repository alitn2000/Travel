using Microsoft.EntityFrameworkCore;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using System.Text.Json.Serialization;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Service;
using Travel.Domain.Services.AppService;
using Travel.EndPoint.JobApi.Test;
using Travel.InfraStructure.EfCore.Common;
using Travel.InfraStructure.EfCore.Repositories;

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

//builder.Services.AddHostedService<QuartzHostedService>();
builder.Services.AddSingleton<IJobFactory, JobFactory>();
builder.Services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
builder.Services.AddScoped<StartTripJob>();
builder.Services.AddScoped<EndTripJob>();
builder.Services.AddScoped<TripJobScheduler>();
builder.Services.AddScoped<ITripJobScheduler, TripJobScheduler>();
 // Add this using directive

//builder.Services.AddQuartz(q =>
//{
    
//    var jobKey = new JobKey("StartTripJob");
//    q.AddJob<StartTripJob>(opts => opts.WithIdentity(jobKey));
//    q.AddTrigger(opts => opts
//        .ForJob(jobKey)
//        .WithIdentity("StartTripJob-trigger")
//        .WithCronSchedule("0 * * ? * *"));

//});
//builder.Services.AddQuartzHostedService(opt => opt.WaitForJobsToComplete = true);
builder.Services.AddScoped<StartTripJob>();
builder.Services.AddScoped<EndTripJob>();
builder.Services.AddControllers()
    .AddJsonOptions(op =>
    {
        op.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var Connection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(Connection));
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
