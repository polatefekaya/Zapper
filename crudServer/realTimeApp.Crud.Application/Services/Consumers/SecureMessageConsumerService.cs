using MassTransit;
using realTimeApp.Crud.Application.Interfaces.Consumers;
using realTimeApp.Crud.Domain.Data.Entities;

namespace realTimeApp.Crud.Application.Services.Consumers;

public class SecureMessageConsumerService : IConsumer<SecureMessageEntity>, ISecureMessageConsumerService
{
    public async Task Consume(ConsumeContext<SecureMessageEntity> context)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"Message Received: {context.Message.Body}");
    }
}
