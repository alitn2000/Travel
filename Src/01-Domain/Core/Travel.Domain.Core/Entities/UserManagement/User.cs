using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Travel.Domain.Core.BaseEntities;
using Travel.Domain.Core.Contracts.BaseContracts;
using Travel.Domain.Core.DTOs.Profile;
using Travel.Domain.Core.Enums;
using Travel.Domain.Core.Events;

namespace Travel.Domain.Core.Entities.UserManagement;

public class User : BaseEntity, IAggrigateRoot
{
    public int Id { get; set; }
    public string UserName { get;private set; }
    public UserNameEnum UserNameType { get;private set; }

    public User() { }

    public User(string userName, UserNameEnum userNameType)
    {
        UserName = userName;
        UserNameType = userNameType;
        AddDomainEvent(new RegisterUserEvent(Id));
    }

    public Profile Profile { get; set; }
    public List<UserTrip> UserTrips { get; set; } = new List<UserTrip>();

    public void UpdateProfile(string? firstName, string? lastName, int? age, GenderEnum? gender, string? address) 
    {
        Profile.UpdateProfile(firstName, lastName, age, gender, address);
    }

    public void CreateProfile()
    {
        if (Profile != null)
            throw new InvalidOperationException("User already has a profile.");

        Profile = new Profile(Id, null, null, null, null, null,null);
    }
    public void RegisterUserCompleted()
    {
        AddDomainEvent(new RegisterUserEvent(Id));
    }
}




