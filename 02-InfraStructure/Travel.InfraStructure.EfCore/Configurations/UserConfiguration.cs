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

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        
        //builder.HasCheckConstraint("CK_UserName_Format",
        //    "[UserNameType] = 1 AND [UserName] LIKE '+989[0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9][0-9]' AND");

        builder.HasData(new List<User>()
        {
            new() { Id = 1, UserName = "+989102123542", UserNameType = UserNameEnum.Mobile },
            new() { Id = 2, UserName = "alitahmasebinia@gmail.com", UserNameType = UserNameEnum.Email},
            new() { Id = 3, UserName = "+989102123543", UserNameType = UserNameEnum.Mobile},
        });
      
    }
}
