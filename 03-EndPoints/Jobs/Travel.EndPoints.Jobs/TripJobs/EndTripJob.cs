using Microsoft.Extensions.Logging;
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

namespace Travel.EndPoints.Jobs.TripJobs
{
    public class EndTripJob : IJob
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EndTripJob> _logger;   
        private readonly IUnitOfWork _unitOfWork;
        public EndTripJob(AppDbContext context, ILogger<EndTripJob> logger, IUnitOfWork unitOfWork)
        {
            _context = context;
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var tripId = context.MergedJobDataMap.GetInt("TripId");

            CancellationToken cancellationToken = context.CancellationToken;

            var trip = await _context.Trips.FindAsync(tripId);

            _logger.LogInformation($"StartTripJob with id tripId==================================================>{tripId} at {DateTime.Now}");
            if (trip == null || trip.Status != StatusEnum.InTrip)
                return;

            if (trip != null && trip.Status == StatusEnum.InTrip && trip.End <= DateTime.Now)
            {
                trip.UpdateStatus(StatusEnum.Ended);
                await _unitOfWork.Commit(trip.CreatedUserId,cancellationToken);
            }
           
            
        }
    }


    }

