using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.DTOs.CheckListTripDtos
{
    public class CheckListTripListDto
    {
        public int Id { get; set; }
        public bool IsChecked { get; set; }
        public int TripId { get; set; }
        public int CheckListId { get; set; }
    }
}
