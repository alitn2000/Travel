using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Travel.Domain.Core.Common;

public interface IBuisnessRule
{
    bool IsBroken();
    public string Message { get; }
}
