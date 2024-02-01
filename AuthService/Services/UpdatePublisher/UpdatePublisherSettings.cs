namespace AuthService.Services.UpdatePublisher;

public class UpdatePublisherSettings
{
    public string UserName { get; set; } = null!;
    public string HostName { get; set; } = null!;
    public int? Port { get; set; }
    
    public string Password => System.Environment.GetEnvironmentVariable("RABBITMQ_PASSWORD") ??
                       throw new KeyNotFoundException("RABBITMQ_PASSWORD variable not set");
}