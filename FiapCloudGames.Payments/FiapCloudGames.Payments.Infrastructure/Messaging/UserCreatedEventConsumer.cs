using FiapCloudGames.Payments.Domain.Entities;
using FiapCloudGames.Payments.Domain.Events;
using FiapCloudGames.Payments.Infrastructure.Data;
using MassTransit;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class UserCreatedEventConsumer(AppDbContext context) : IConsumer<UserCreatedEvent>
{
    public async Task Consume(ConsumeContext<UserCreatedEvent> ctx)
    {
        var msg = ctx.Message;

        Conta conta = new()
        {
            UsuarioId = msg.UsuarioId,
            Saldo = 1500
        };

        context.Contas.Add(conta);
        await context.SaveChangesAsync(ctx.CancellationToken);
    }
}
