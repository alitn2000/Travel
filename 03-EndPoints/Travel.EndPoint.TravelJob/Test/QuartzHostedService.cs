//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.Extensions.Hosting;
//using Quartz;
//using Quartz.Spi;

//namespace Travel.EndPoint.TravelJob.Test;

//public class QuartzHostedService : IHostedService
//{
//    ISchedulerFactory _schedulerFactory; // baraye sakhte scheduler ha estefade mishavad
//    IJobFactory _jobFactory; // baraye sakhte job ha estefade mishavad
//    List<Job> _myJobs; // listi az job ha ke bayad ejra shavand

//    public QuartzHostedService(ISchedulerFactory schedulerFactory, IJobFactory jobFactory, List<Job> myJobs)
//    {
//        _jobFactory = jobFactory;
//        _schedulerFactory = schedulerFactory;
//        _myJobs = myJobs;
//    }
//    public IScheduler Scheduler { get; set; }  // baraye negahdari va piyade sazi


//    public async Task StartAsync(CancellationToken cancellationToken)  //baraye ejra kardan service ha dar waqt start shodan application estefade mishavad
//    {
//        Scheduler = await _schedulerFactory.GetScheduler(cancellationToken); // sakhte scheduler bar asase schedulerFactory

//        Scheduler.JobFactory = _jobFactory; // moshakhas kardan jobFactory baraye scheduler

//        foreach (var myJob in _myJobs) // barresi har job dar listi ke moshakhas shode
//        {
//            var job = CreateJob(myJob); // sakhte job bar asase type moshakhas shode dar myJob
//            var trigger = CreateTrigger(myJob); // sakhte trigger bar asase expression moshakhas shode dar myJob
//            await Scheduler.ScheduleJob(job, trigger, cancellationToken); // schedule kardan job va trigger bar asase scheduler
//        }

//        await Scheduler.Start(cancellationToken); // shoroo kardan scheduler bar asase cancellationToken
//    }

//    public async Task StopAsync(CancellationToken cancellationToken) // baraye ejra kardan service ha dar waqt stop shodan application estefade mishavad
//    {
//       Scheduler?.Shutdown(cancellationToken); // shutdown kardan scheduler
//    }

//    private static IJobDetail CreateJob(Job myJob)
//    {
//        var type = myJob.Type;
//        return JobBuilder.Create(type) // sakhte job bar asase type moshakhas shode dar myJob
//            .WithIdentity(type.FullName) // moshakhas kardan yeki az moshakhasat job bar asase type
//            .WithDescription(type.Name) // moshakhas kardan tozihate job bar asase type
//            .Build();
//    }

//    private static ITrigger CreateTrigger(Job myJob)
//    {
//        var type = myJob.Type;
//        return TriggerBuilder.Create() // sakhte trigger bar asase moshakhasat job
//            .WithIdentity($"{myJob.Type.FullName}.trigger") // moshakhas kardan yeki az moshakhasat trigger bar asase type
//            .WithCronSchedule(myJob.Experssion) // moshakhas kardan zamanbadi bar asase expression moshakhas shode dar myJob
//            .WithDescription(myJob.Experssion) // moshakhas kardan tozihate trigger bar asase type
//            .Build();
//    }
//}
