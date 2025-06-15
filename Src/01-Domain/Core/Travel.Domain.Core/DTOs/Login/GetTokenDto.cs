using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.DTOs.Login;

public class GetTokenDto
{
    [Required(ErrorMessage = "Username is required.")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "OTP is required.")]
    public string OTP { get; set; }
}
