namespace OrdersManagement.Application.Common.Configuration;

public class JWTSettings
{
    public const string SectionName = "JWTSettings";
    
    public string SecretKey { get; set; } = string.Empty;
    public string[] ValidIssuers { get; set; } = Array.Empty<string>();
    public string[] ValidAudiences { get; set; } = Array.Empty<string>();
    public int ExpirationInDays { get; set; } = 7;
}
