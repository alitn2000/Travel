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

namespace Travel.EndPoint.TravelJob;
[DisallowConcurrentExecution]
public class StartTripJob : IJob
{
    private readonly AppDbContext _context;

    public StartTripJob(AppDbContext context)
    {
        _context = context;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            var tripId = context.MergedJobDataMap.GetInt("TripId");

            var trip = await _context.Trips.FindAsync(tripId);

            if (trip == null || trip.Status != StatusEnum.Pending)
                return;

            trip.Status = StatusEnum.InTrip;
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            Console.WriteLine($"Error in StartTripJob: {ex.Message}");
        }

    }
}
