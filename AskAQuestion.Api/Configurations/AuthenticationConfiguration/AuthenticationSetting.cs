using System;
namespace AskAQuestion.Api.Configurations.AuthenticationConfiguration;

public class AuthenticationSetting
{
    public string JwtKey { get; set; }
    public int JwtExpireDays { get; set; }
    public string JwtIssuer { get; set; }
}

