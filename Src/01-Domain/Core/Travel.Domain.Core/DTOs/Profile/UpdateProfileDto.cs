using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Entities;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.DTOs.Profile;

public class UpdateProfileDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int? Age { get; set; }
    [Range(1,2, ErrorMessage =" gender not in range 1-2 or male or female")]
    public GenderEnum? Gender { get; set; }
    public string? Address { get; set; }
   
}
