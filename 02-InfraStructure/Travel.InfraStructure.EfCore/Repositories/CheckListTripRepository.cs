using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class CheckListTripRepository : ICheckListTripRepository
{
    private readonly AppDbContext _context;

    public CheckListTripRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UpdateIsChecked(UpdateCheckListTripDto dto, CancellationToken cancellationToken)
    {
        var chTrip = await _context.CheckListTrips
            .FirstOrDefaultAsync(c => c.TripId == dto.TripId && c.CheckListId == dto.CheckListId, cancellationToken);

        if (chTrip == null)
            return false;

        chTrip.IsChecked = dto.IsChecked;
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }
}
