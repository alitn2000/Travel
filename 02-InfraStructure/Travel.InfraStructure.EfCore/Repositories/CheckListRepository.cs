using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Contracts.Repositories;
using Travel.Domain.Core.Entities;
using Travel.InfraStructure.EfCore.Common;

namespace Travel.InfraStructure.EfCore.Repositories;

public class CheckListRepository : ICheckListRepository
{
    private readonly AppDbContext _context;

    public CheckListRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<CheckList>> GetAllCheckListsAsync(CancellationToken cancellationToken)
        => await _context.CheckLists.ToListAsync(cancellationToken);

    public async Task<bool> CheckCheckListExist(int checkListId, CancellationToken cancellationToken)
       =>  await _context.CheckLists.AsNoTracking().AnyAsync(c => c.Id == checkListId, cancellationToken);
    
}
