using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Travel.Domain.Core.Common;
using Travel.Domain.Core.Entities.UserManagement.Rules;
using Travel.Domain.Core.Exceptions;

namespace Travel.Domain.Core.Entities.UserManagement.ValueObjects;

public class PhoneNumber
{
    public string Value { get; private set; }

    public PhoneNumber(string value)
    {
        CheckRule(new PhoneNumberRule(value));

        Value = Normalize(value);
    }

    private void CheckRule(IBuisnessRule rule)
    {
        if (rule.IsBroken())
            throw new BuisnessRuleException(rule.Message);
    }

    private string Normalize(string value)
    {
        if (value.StartsWith("0"))
            return "+98" + value.Substring(1);
        return value;
    }

    public override string ToString() => Value;

    protected bool Equals(PhoneNumber other) => Value == other.Value;

    public override bool Equals(object? obj) => obj is PhoneNumber other && Equals(other);

    public override int GetHashCode() => Value.GetHashCode();
}
