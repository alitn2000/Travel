using Quartz;
using Quartz.Spi;
using Travel.Domain.Core.Contracts.Jobs;
using Travel.EndPoint.TravelJob;
namespace TestApi.test;

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

        if (!scheduler.IsStarted)
            await scheduler.Start();

        var startJob = JobBuilder.Create<StartTripJob>()
            .WithIdentity($"StartTripJob_{tripId}")
            .UsingJobData("TripId", tripId)
            .StoreDurably()
            .Build();

        var startTrigger = TriggerBuilder.Create()
            .WithIdentity($"StartTrigger_{tripId}")
            .StartAt(startTime)
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