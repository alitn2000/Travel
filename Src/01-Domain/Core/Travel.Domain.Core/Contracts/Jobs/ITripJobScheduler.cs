using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Contracts.Jobs
{
    public interface ITripJobScheduler
    {
        Task ScheduleTripJobsAsync(int tripId, DateTime startTime, DateTime endTime);
    }
}
