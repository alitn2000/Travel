using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Enums;

namespace Travel.InfraStructure.EfCore.Configurations;

public class CheckListConfiguration : IEntityTypeConfiguration<CheckList>
{
    public void Configure(EntityTypeBuilder<CheckList> builder)
    {
        builder.HasData(new List<CheckList>()
        {
            new() { Id = 1, ChekListType = CheckListEnums.Common, TripType = TripEnums.Cultural },
            new() { Id = 2, ChekListType = CheckListEnums.Specific, TripType = TripEnums.Nature },
            new() { Id = 3, ChekListType = CheckListEnums.Common, TripType = TripEnums.Pilgrimage },
            new() { Id = 4, ChekListType = CheckListEnums.Specific, TripType = TripEnums.Adventure },
            new() { Id = 5, ChekListType = CheckListEnums.Common, TripType = TripEnums.Relaxation },
        });
    }
}
