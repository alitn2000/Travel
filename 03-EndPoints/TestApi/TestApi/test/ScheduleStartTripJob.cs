//using Microsoft.EntityFrameworkCore.Metadata.Internal;
//using Quartz;
//using Travel.EndPoint.TravelJob;
//using static Quartz.Logging.OperationName;

//namespace TestApi.test;

//public class ScheduleStartTripJob
//{
//      private readonly IScheduler _scheduler;

    
//    public ScheduleStartTripJob(IScheduler scheduler)
//    {
//        _scheduler = scheduler;
//    }

//    public async Task Schedule(int tripId)
//    {
//        var job = JobBuilder.Create<StartTripJob>()
//            .WithIdentity($"StartTripJob_{tripId}")
//            .UsingJobData("TripId", tripId)
//            .Build();

//        var trigger = TriggerBuilder.Create()
//            .ForJob(job)
//            .WithIdentity($"StartTripTrigger_{tripId}")
//            .StartNow()
//            .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
//            .Build();

//        await _scheduler.ScheduleJob(job, trigger);
//    }
//}
