using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.EndPoint.JobApi.Test;

public class JobFactory : IJobFactory
{
    private IServiceProvider _serviceProvider; 

    public JobFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
    {
        return _serviceProvider.GetRequiredService(bundle.JobDetail.JobType) as IJob; // baraye sakhte job az service provider estefade mikonim
    }

    public void ReturnJob(IJob job)
    {
        
    }
}
