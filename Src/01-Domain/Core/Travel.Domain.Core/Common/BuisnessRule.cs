using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Common;

public abstract class BuisnessRule : IBuisnessRule
{
    public abstract bool IsBroken();
    public abstract string Message { get; }
}
