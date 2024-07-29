using System.Text;
using MassTransit;
using realTimeApp.Crud.Application.Interfaces.Consumers;
using realTimeApp.Domain.Data.Entities;

namespace realTimeApp.Crud.Application.Services.Consumers;

public class SecureMessageConsumerService : IConsumer<SecureMessageEntity>, ISecureMessageConsumerService
{
    public async Task Consume(ConsumeContext<SecureMessageEntity> context)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Message Received: {Encoding.UTF8.GetString(context.Message.Body)}");
    }
}
