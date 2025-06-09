using Microsoft.IdentityModel.Tokens;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Enums;
using Travel.InfraStructure.EfCore.Common;
using Travel.InfraStructure.EfCore.Repositories;

namespace Travel.EndPoint.TravelJob;

public class StartTripJob : IJob
{
    private readonly AppDbContext _context;
    private readonly ILogger<StartTripJob> _logger;


    public StartTripJob(AppDbContext context, ILogger<StartTripJob> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      
        var tripId = context.MergedJobDataMap.GetInt("TripId");

        _logger.LogInformation($"StartTripJob with id tripId=========================================================================================>{tripId} at {DateTime.Now}");

        var trip = await _context.Trips.FindAsync(tripId);

            if (trip == null)
                return;
        if (trip != null && trip.Status == StatusEnum.Pending && trip.Start <= DateTime.Now)
        {
            trip.Status = StatusEnum.InTrip;
            await _context.SaveChangesAsync();
        }
        
           


    }
}
