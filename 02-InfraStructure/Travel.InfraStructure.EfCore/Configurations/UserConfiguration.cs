using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities;

namespace Travel.InfraStructure.EfCore.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        builder.Property(u => u.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder.HasData(new List<User>()
        {
            new() { Id = 1, Name = "name1", Email = "test1@gmail.com", Password = "123"},
            new() { Id = 2, Name = "name2", Email = "test2@gmail.com", Password = "123"},
            new() { Id = 3, Name = "name3", Email = "test3@gmail.com", Password = "123"}
        });
      
    }
}
