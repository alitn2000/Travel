using Microsoft.Extensions.Logging;
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

namespace Travel.EndPoints.Jobs.TripJobs;

public class StartTripJob : IJob
{
    private readonly AppDbContext _context;
    private readonly ILogger<StartTripJob> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public StartTripJob(AppDbContext context, ILogger<StartTripJob> logger, IUnitOfWork unitOfWork)
    {
        _context = context;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Execute(IJobExecutionContext context)
    {
      
        var tripId = context.MergedJobDataMap.GetInt("TripId");
        CancellationToken cancellationToken = context.CancellationToken;
        _logger.LogInformation($"StartTripJob with id tripId=========================================================================================>{tripId} at {DateTime.Now}");

        var trip = await _context.Trips.FindAsync(tripId);

            if (trip == null)
                return;
        if (trip != null && trip.Status == StatusEnum.Pending && trip.Start <= DateTime.Now)
        {
            trip.UpdateStatus(StatusEnum.InTrip);
            await _unitOfWork.Commit(trip.CreatedUserId,cancellationToken);
        }
        
           


    }
}
