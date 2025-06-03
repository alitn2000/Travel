﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.BaseEntities;

public class Result
{
    public bool Flag { get; set; }
    public string? Message { get; set; }
    public Result(bool flag, string message)
    {
        Flag = flag;
        Message = message;
    }
}
