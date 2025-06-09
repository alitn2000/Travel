using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Jobs;

namespace Travel.EndPoint.TravelJob;

public class TripJobScheduler : ITripJobScheduler
{
    private readonly ISchedulerFactory _schedulerFactory;
    private readonly IJobFactory _jobFactory;

    public TripJobScheduler(ISchedulerFactory schedulerFactory, IJobFactory jobFactory)
    {
        _schedulerFactory = schedulerFactory;
        _jobFactory = jobFactory;
    }

   public async Task ScheduleTripJobsAsync(int tripId, DateTime startTime, DateTime endTime)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        scheduler.JobFactory = _jobFactory;

        var startJob = JobBuilder.Create<StartTripJob>() //baraye sakhte job bar asase StartTripJob
            .WithIdentity($"StartTripJob_{tripId}")  //shenase monhaser be fard job bar asase tripId
            .UsingJobData("TripId", tripId)
            .Build();

        var startTrigger = TriggerBuilder.Create()
            .WithIdentity($"StartTrigger_{tripId}")  //shenase
            .StartAt(startTime)   //zaman shoroo kardan job bar asase startTime
            .Build();


        var endJob = JobBuilder.Create<EndTripJob>()
            .WithIdentity($"EndTripJob_{tripId}")
            .UsingJobData("TripId", tripId)
            .Build();

        var endTrigger = TriggerBuilder.Create()
            .WithIdentity($"EndTrigger_{tripId}")
            .StartAt(endTime)
            .Build();

        await scheduler.ScheduleJob(startJob, startTrigger);
        await scheduler.ScheduleJob(endJob, endTrigger);
    }

}
