using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.EndPoint.TravelJob;

namespace TestApi.test;

public static class DependencyInjection
{
    public static void AddInfraStructure(this IServiceCollection services)
    {
        services.AddQuartz();


        services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);
       
    }
}


