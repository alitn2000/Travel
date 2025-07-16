using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Exceptions;

public class BuisnessRuleException : Exception
{
    public BuisnessRuleException(string message) : base(message) { }
    public BuisnessRuleException() : base("A buisness rule exception occured") { }
}
