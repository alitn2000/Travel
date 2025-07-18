﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.DTOs.CheckListDtos;
using Travel.Domain.Core.Entities.CheckListManagement;
using Travel.Domain.Core.Enums;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class CheckListRepository : ICheckListRepository
{
    private readonly AppDbContext _context;
    private readonly IUnitOfWork _unitOfWork;

    public CheckListRepository(AppDbContext context, IUnitOfWork unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }

    public async Task<List<CheckListListsDto>> GetAllCheckListsAsync(CancellationToken cancellationToken)
        => await _context.CheckLists
        .Select(c => new CheckListListsDto
        {
            Id = c.Id,
            TripType = c.TripType,
            ChekListType = c.ChekListType
            
        })
        .ToListAsync(cancellationToken);

    public async Task<bool> CheckCheckListExist(int checkListId, CancellationToken cancellationToken)
       =>  await _context.CheckLists.AsNoTracking().AnyAsync(c => c.Id == checkListId, cancellationToken);

    public async Task<bool> AddCheckList(CheckList checkList,int userId, CancellationToken cancellationToken)
    {
        await _context.CheckLists.AddAsync(checkList, cancellationToken);

        return await _unitOfWork.Commit(userId,cancellationToken) > 0;
    }

    public async Task<bool> CheckTypesExists(string chListEnum, TripEnums tripEnum, CancellationToken cancellationToken)
        => await _context.CheckLists
            .AsNoTracking()
            .AnyAsync(c => c.TripType == tripEnum && c.ChekListType == chListEnum);
    
    public async Task<Result> UpdateCheckList(CheckList checkList,int userId, CancellationToken cancellationToken)
    {
        _context.Update(checkList);

        var result = await _unitOfWork.Commit(userId, cancellationToken);
        if( result <= 0)
            return new Result(false, "Failed to update checkList!!!");
        return new Result(true, "checkList updated successfully!!!");
    }

    public async Task<CheckList?> GetCheckListById(int checkListId, CancellationToken cancellationToken)
    
        => await _context.CheckLists
            .FirstOrDefaultAsync(c => c.Id == checkListId, cancellationToken);
    
}
