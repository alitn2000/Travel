﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.DTOs.CheckListDtos;

public class CheckListListsDto
{
    public int Id { get; set; }
    public string ChekListType { get; set; }
    public TripEnums TripType { get; set; }
}
