using FiapCloudGames.Contracts.Events;
using FiapCloudGames.Payments.Domain.Entities;
using FiapCloudGames.Payments.Infrastructure.Data;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class UserCreatedEventConsumer(
    ILogger<UserCreatedEventConsumer> logger,
    AppDbContext context) : IConsumer<UserCreatedEvent>
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

        logger.LogInformation("Dados da conta do usuário {MsgNome} cadastrados com sucesso", msg.Nome);
    }
}
