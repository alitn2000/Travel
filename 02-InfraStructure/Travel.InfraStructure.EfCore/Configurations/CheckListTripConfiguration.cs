using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities;

namespace Travel.InfraStructure.EfCore.Configurations;

public class CheckListTripConfiguration : IEntityTypeConfiguration<CheckListTrip>
{
    public void Configure(EntityTypeBuilder<CheckListTrip> builder)
    {
        builder.HasData(new List<CheckListTrip>()
        {
            new() { Id = 1, CheckListId = 1, TripId = 1 },
            new() { Id = 2, CheckListId = 2, TripId = 2 },
            new() { Id = 3, CheckListId = 3, TripId = 3 },
        });
    }
}
