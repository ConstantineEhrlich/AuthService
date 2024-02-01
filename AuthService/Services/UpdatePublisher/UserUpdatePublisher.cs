using System.Text;
using System.Text.Json;
using AuthService.Models;
using AuthService.Services.DTO;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace AuthService.Services.UpdatePublisher;

public class UserUpdatePublisher: IUserUpdatePublisher
{
    private readonly ILogger<User> _logger;
    private readonly IModel _channel;
    private readonly string _name;

    public UserUpdatePublisher(ILogger<User> logger, IOptions<UpdatePublisherSettings> settings)
    {
        _logger = logger;
        ConnectionFactory factory = new();
        factory.UserName = settings.Value.UserName;
        factory.Password = settings.Value.Password;
        factory.HostName = settings.Value.HostName;
        factory.Port = settings.Value.Port ?? 5555;
        IConnection connection = factory.CreateConnection();
        _channel = connection.CreateModel();
        _name = this.GetType().Name;
        _channel.QueueDeclare(queue: _name,
                              durable: true,
                              exclusive:false,
                              autoDelete:false,
                              arguments:null);
    }

    public void NewUserCreated(SignUp signUpData)
    {
        object messageObject = new
        {
            Id = signUpData.Login,
            Name = signUpData.Name,
            Email = signUpData.Email,
        };

        string messageJson = JsonSerializer.Serialize(messageObject);
        byte[] body = Encoding.UTF8.GetBytes(messageJson);

        _channel.BasicPublish(exchange: string.Empty,
            routingKey: _name,
            basicProperties: null,
            body: body);
    }
}