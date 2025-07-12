using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Entities;

public class Profile : BaseEntity
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    public GenderEnum? Gender { get; set; }
    public string? Address { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
