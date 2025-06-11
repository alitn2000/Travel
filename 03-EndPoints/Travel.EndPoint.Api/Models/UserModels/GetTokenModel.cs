using Travel.Domain.Core.Enums;

namespace Travel.EndPoint.Api.Models.UserModels;

public class GetTokenModel
{

    public string UserName { get; set; }
    public UserNameEnum UserNameType { get; set; }
    public string OTP { get; set; }
}
