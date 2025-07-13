using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities.CheckListManagement;

namespace Travel.InfraStructure.EfCore.Configurations;

public class CheckListTripConfiguration : IEntityTypeConfiguration<CheckListTrip>
{
    public void Configure(EntityTypeBuilder<CheckListTrip> builder)
    {
        builder.HasKey(ct => ct.Id);
    }
}
