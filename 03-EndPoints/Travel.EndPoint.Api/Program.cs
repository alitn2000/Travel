using Microsoft.EntityFrameworkCore;
using Travel.Domain.Core.Contracts.AppServices;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Contracts.Services;
using Travel.Domain.Service;
using Travel.Domain.Services.AppService;
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

builder.Services.AddControllers();
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
