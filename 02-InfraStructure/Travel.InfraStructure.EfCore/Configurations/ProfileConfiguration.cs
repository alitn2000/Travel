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

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasOne(p => p.User)
        .WithOne(u => u.Profile)
        .HasForeignKey<Profile>(p => p.UserId)
        .IsRequired();


        builder.Property(p => p.FirstName).HasMaxLength(15).IsRequired(false);
        builder.Property(p => p.LastName).HasMaxLength(25).IsRequired(false);
        builder.Property(p => p.Age).IsRequired(false);

        builder.HasData(new List<Profile>()
        {
            new() { Id = 1, UserId = 1, FirstName = "Ali", LastName = "Tahmasebinia", Age = 25, Address = "SomeWhere", Gender = GenderEnum.Male},
            new() { Id = 2, UserId = 2, FirstName = "Alireza", LastName = "Tahmasebinia", Age = 26, Address = "SomeWhere", Gender = GenderEnum.Male},
            new() { Id = 3, UserId = 3, FirstName = "Sepide", LastName = "Sepidei", Age = 25, Address = "SomeWhere", Gender = GenderEnum.Female},
        });
    }
}
