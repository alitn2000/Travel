using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Events;

public class RegisterUserEvent : INotification
{
    public int UserId { get; set; }
    public RegisterUserEvent(int userId)
    {
        UserId = userId;
    }

}
