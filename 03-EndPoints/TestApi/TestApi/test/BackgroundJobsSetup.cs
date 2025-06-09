//using Microsoft.Extensions.Options;
//using Quartz;
//using Travel.EndPoint.TravelJob;
//namespace TestApi.test;

//public class BackgroundJobsSetup : IConfigureOptions<QuartzOptions>
//{
//    public void Configure(QuartzOptions options)
//    {
//        var jobKeyStart = JobKey.Create(nameof(StartTripJob));
//        var jobKeyEnd = JobKey.Create(nameof(EndTripJob));

        
//        options.AddJob<StartTripJob>(jobBuilder => jobBuilder.WithIdentity(jobKeyStart));
//        options.AddJob<EndTripJob>(jobBuilder => jobBuilder.WithIdentity(jobKeyEnd));
//    }
//}
