using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.DTOs.CheckListDtos;

public class UpdateCheckListDto
{
    [Required(ErrorMessage = "id required")]
    public int Id { get; set; }
    [Required(ErrorMessage = "check list type required")]
    public string ChekListType { get; set; }
    [Required(ErrorMessage = "trip type type required")]
    public TripEnums TripType { get; set; }
}
