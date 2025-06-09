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

namespace Travel.EndPoint.JobApi.Test
{
    public class EndTripJob : IJob
    {
        private readonly AppDbContext _context;

        public EndTripJob(AppDbContext context)
        {
            _context = context;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var tripId = context.MergedJobDataMap.GetInt("TripId");

            var trip = await _context.Trips.FindAsync(tripId);
            if (trip == null || trip.Status != StatusEnum.InTrip)
                return;

            trip.Status = StatusEnum.Ended;
            await _context.SaveChangesAsync();
        }
    }


    }

