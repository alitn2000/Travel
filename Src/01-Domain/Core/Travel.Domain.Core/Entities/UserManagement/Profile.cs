using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Entities.UserManagement.Rules;
using Travel.Domain.Core.Entities.UserManagement.ValueObjects;
using Travel.Domain.Core.Enums;

namespace Travel.Domain.Core.Entities.UserManagement;

public class Profile : BaseEntity
{
    public int Id { get; set; }
    public string? FirstName { get; private set; }
    public string? LastName { get; private set; }
    public int? Age { get; private set; }
    public GenderEnum? Gender { get; private set; }
    public string? Address { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    public PhoneNumber? PhoneNumber { get; private set; }

    public Profile() { }

    

    public Profile(int userId, string? firstName, string? lastName, int? age, GenderEnum? gender, string? address,PhoneNumber? phoneNumber)
    {
        UserId = userId;
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Gender = gender;
        Address = address;
        PhoneNumber = phoneNumber;
    }
    public void UpdateProfile(string? firstName, string? lastName, int? age, GenderEnum? gender, string? address)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        Gender = gender;
        Address = address;
    }

    public void UpdatePhoneNumber(string phoneNumber)
    {
        CheckRules(new PhoneNumberRule(phoneNumber));

        PhoneNumber = new PhoneNumber(phoneNumber);
    }
}
