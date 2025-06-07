using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.DTOs.CheckListTripDtos;

public class CheckListIsCheckedDto
{
    public int CheckListId { get; set; }
    public bool IsChecked { get; set; }
}
