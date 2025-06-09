//using Quartz;
//using Travel.EndPoint.TravelJob;

//namespace TestApi.test;

//public class ScheduleEndTripJob
//{
//    private readonly IScheduler _scheduler;
//    public ScheduleEndTripJob(IScheduler scheduler)
//    {
//        _scheduler = scheduler;
//    }
//    public async Task Schedule(int tripId)
//    {
//        var job = JobBuilder.Create<EndTripJob>()
//            .WithIdentity($"EndTripJob_{tripId}")
//            .UsingJobData("TripId", tripId)
//            .Build();
//        var trigger = TriggerBuilder.Create()
//            .ForJob(job)
//            .WithIdentity($"EndTripTrigger_{tripId}")
//            .StartNow()
//            .WithSimpleSchedule(x => x.WithIntervalInSeconds(10).RepeatForever())
//            .Build();
//        await _scheduler.ScheduleJob(job, trigger);
//    }
//}
