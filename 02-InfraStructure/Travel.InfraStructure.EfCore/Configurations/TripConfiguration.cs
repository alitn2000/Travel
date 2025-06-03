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

public class TripConfiguration : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasData(new List<Trip>()
        {
            new() { Id = 1, Destination = "tehran", Start = new DateTime(2025, 5, 6, 12, 0, 0), End = new DateTime(2025, 5, 9, 12, 0, 0), TripType = TripEnums.Cultural, UserId = 1 },
            new() { Id = 2, Destination = "ardabil", Start = new DateTime(2025, 5, 6, 12, 0, 0), End = new DateTime(2025, 5, 9, 12, 0, 0), TripType = TripEnums.Nature, UserId = 2 },
            new() { Id = 3, Destination = "mashhad", Start = new DateTime(2025, 5, 6, 12, 0, 0), End = new DateTime(2025, 5, 9, 12, 0, 0), TripType = TripEnums.Pilgrimage, UserId = 3 }
        });
    }
}
