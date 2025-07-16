using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Service.Exceptions
{
    public class CommandValidationException : Exception
    {
        public CommandValidationException(string message) : base(message)
        {
        }
        public CommandValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }
        public CommandValidationException() : base("Command validation failed.") { }
    }
}
