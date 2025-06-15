using Travel.Domain.Core.Enums;

namespace Travel.EndPoint.Api.Models.UserModels;

public class UserLoginModel
{

    public string UserName { get; set; }
    public UserNameEnum UserNameType { get; set; }
}
