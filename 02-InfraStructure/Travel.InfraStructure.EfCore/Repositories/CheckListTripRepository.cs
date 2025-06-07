using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.CheckListTripDtos;
using Travel.Domain.Core.Entities;
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

        _context.CheckListTrips.Where(c => c.TripId == dto.TripId).ExecuteDelete<CheckListTrip>();


        var newCheckListTrips = dto.CheckLists.Select(c => new CheckListTrip
        {
            TripId = dto.TripId,
            CheckListId = c.CheckListId,
            IsChecked = c.IsChecked
        }).ToList();


        await _context.CheckListTrips.AddRangeAsync(newCheckListTrips, cancellationToken);

        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<CheckListTripListDto>> GetAllCheckListTrips(CancellationToken cancellationToken)
        => await _context.CheckListTrips.Select(c => new CheckListTripListDto
        {
            Id = c.Id,
            TripId = c.TripId,
            CheckListId = c.CheckListId,
            IsChecked = c.IsChecked
        }).ToListAsync(cancellationToken);
    public async Task<bool> AddCheckListTrip(AddCheckListToTripDto dto, CancellationToken cancellationToken)
    {


        var existCheckLists = await _context
            .CheckLists
            .Where(c => dto.CheckListIds.Contains(c.Id))
            .Select(c => c.Id)
            .ToListAsync(cancellationToken);

        var notExistCheckLists = dto.CheckListIds.Except(existCheckLists).ToList();

        if (notExistCheckLists.Any())
            return false;

        var checkListTrips = existCheckLists.Select(checkListId => new CheckListTrip
        {
            TripId = dto.TripId,
            CheckListId = checkListId,
            IsChecked = false
        }).ToList();

        await _context.CheckListTrips.AddRangeAsync(checkListTrips, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    public async Task<List<CheckListTripListDto>> GetAllIsCheckedLists(CancellationToken cancellationToken)
       => await _context.CheckListTrips
            .Where(c => c.IsChecked == true)
            .Select(c => new CheckListTripListDto
            {
                Id = c.Id,
                TripId = c.TripId,
                CheckListId = c.CheckListId,
                IsChecked = c.IsChecked
            }).ToListAsync(cancellationToken);

}
