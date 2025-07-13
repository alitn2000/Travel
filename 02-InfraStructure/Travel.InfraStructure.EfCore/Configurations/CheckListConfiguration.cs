using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities.CheckListManagement;
using Travel.Domain.Core.Enums;

namespace Travel.InfraStructure.EfCore.Configurations;

public class CheckListConfiguration : IEntityTypeConfiguration<CheckList>
{
    public void Configure(EntityTypeBuilder<CheckList> builder)
    {
        builder.HasData(new List<CheckList>()
        {
            new() { Id = 1, ChekListType ="A",TripType = TripEnums.Cultural },
            new() { Id = 2, ChekListType ="B", TripType = TripEnums.Nature },
            new() { Id = 3, ChekListType ="C",TripType = TripEnums.Pilgrimage },
            new() { Id = 4, ChekListType ="D", TripType = TripEnums.Adventure },
            new() { Id = 5, ChekListType ="F",TripType = TripEnums.Relaxation },
        });
    }
}
