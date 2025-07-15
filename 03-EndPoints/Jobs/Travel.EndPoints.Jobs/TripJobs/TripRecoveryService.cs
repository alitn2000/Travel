using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Enums;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.EndPoints.Jobs.TripJobs;

public class TripRecoveryService : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<TripRecoveryService> _logger;

    public TripRecoveryService(IServiceProvider serviceProvider, ILogger<TripRecoveryService> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = _serviceProvider.CreateScope();
        var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        var now = DateTime.Now;

        var toStart = await _context.Trips
            .Where(t => t.Status == StatusEnum.Pending && t.Start <= now).ToListAsync(cancellationToken);

        foreach (var trip in toStart)
            trip.UpdateStatus(StatusEnum.InTrip);
            

        var toEnd = await _context.Trips
            .Where(t => t.Status == StatusEnum.InTrip && t.End <= now)
            .ToListAsync();

        foreach (var trip in toEnd)
            trip.UpdateStatus(StatusEnum.Ended);
       

        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation($"[TripStatusRecoveryService] Status updated at {now}.");
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}

