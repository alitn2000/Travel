﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.DTOs.Login;

public class LoginDto
{
    [Required(ErrorMessage = "Email is required.")]
    public string UserName { get; set; }

    public UserNameEnum UserNameType { get; set; }

}
