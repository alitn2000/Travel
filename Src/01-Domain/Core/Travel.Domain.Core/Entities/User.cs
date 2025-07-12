using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Entities;

public class User : BaseEntity
{
    public int Id { get; set; }
    public string UserName { get; set; }
    public UserNameEnum UserNameType { get; set; }


    public Profile Profile { get; set; }
    List<UserTrip> UserTrips { get; set; } = new List<UserTrip>();

}



