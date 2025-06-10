using Travel.Domain.Core.Enums;

namespace Travel.EndPoint.Api.Models.UserModels;

public class UserLoginModel
{
    public UserNameEnum UserNameType { get; set; }
    public string UserName { get; set; }
}
