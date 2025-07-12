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
    private readonly IUnitOfWork _unitOfWork;
    public CheckListTripRepository(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<bool> UpdateIsChecked(UpdateCheckListTripDto dto,int userId, CancellationToken cancellationToken)
    {

        var existingCheckLists = await _context.CheckListTrips
        .Where(c => c.TripId == dto.TripId)
        .ToListAsync(cancellationToken);

        var existingMap = existingCheckLists.ToDictionary(x => x.CheckListId, x => x);

        var toAdd = new List<CheckListTrip>();

        foreach (var item in dto.CheckLists)
        {
            if (existingMap.TryGetValue(item.CheckListId, out var existing))
            {
               
                if (existing.IsChecked != item.IsChecked)
                {
                    existing.IsChecked = item.IsChecked;
                    _context.CheckListTrips.Update(existing);
                }
            }
            else
            {
                
                toAdd.Add(new CheckListTrip
                {
                    TripId = dto.TripId,
                    CheckListId = item.CheckListId,
                    IsChecked = item.IsChecked
                });
            }
        }

        if (toAdd.Any())
            await _context.CheckListTrips.AddRangeAsync(toAdd, cancellationToken);

        await _unitOfWork.Commit(userId,cancellationToken);
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
    public async Task<bool> AddCheckListTrip(AddCheckListToTripDto dto,int userId, CancellationToken cancellationToken)
    {


      
        var existCheckLists = await _context.CheckLists
            .Where(c => dto.CheckListIds.Contains(c.Id))
            .Select(c => c.Id)
            .ToListAsync(cancellationToken);

        var notExistCheckLists = dto.CheckListIds.Except(existCheckLists).ToList();

        if (notExistCheckLists.Any())
            return false; 

        
        var existingCheckListTripIds = await _context.CheckListTrips
            .Where(clt => clt.TripId == dto.TripId && dto.CheckListIds.Contains(clt.CheckListId))
            .Select(clt => clt.CheckListId)
            .ToListAsync(cancellationToken);

        
        var newCheckListIds = existCheckLists.Except(existingCheckListTripIds).ToList();

        if (!newCheckListIds.Any())
            return true; 

        var checkListTrips = newCheckListIds.Select(checkListId => new CheckListTrip
        {
            TripId = dto.TripId,
            CheckListId = checkListId,
            IsChecked = false
        }).ToList();

        await _context.CheckListTrips.AddRangeAsync(checkListTrips, cancellationToken);
        await _unitOfWork.Commit(userId, cancellationToken);
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
