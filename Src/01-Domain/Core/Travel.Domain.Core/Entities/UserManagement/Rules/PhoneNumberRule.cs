using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Travel.Domain.Core.Common;

namespace Travel.Domain.Core.Entities.UserManagement.Rules
{
    public class PhoneNumberRule : BuisnessRule
    {

        private readonly string _phoneNumber;

        public PhoneNumberRule(string phoneNumber)
        {
            _phoneNumber = phoneNumber;
        }

       

        public override bool IsBroken()
        {
            return !Regex.IsMatch(_phoneNumber, @"^(\+98|0)?9\d{9}$");
        } 
        public override string Message => "phone number format is incorrect";
    }
}
