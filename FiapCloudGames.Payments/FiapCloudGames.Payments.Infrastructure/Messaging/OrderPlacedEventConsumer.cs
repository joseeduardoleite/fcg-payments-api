using FiapCloudGames.Contracts.Events;
using FiapCloudGames.Payments.Domain.Entities;
using FiapCloudGames.Payments.Domain.Enums;
using FiapCloudGames.Payments.Infrastructure.Data;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Infrastructure.Messaging;

[ExcludeFromCodeCoverage]
public class OrderPlacedEventConsumer(
    AppDbContext context,
    IPublishEndpoint publishEndpoint) : IConsumer<OrderPlacedEvent>
{
    public async Task Consume(ConsumeContext<OrderPlacedEvent> contextMsg)
    {
        OrderPlacedEvent message = contextMsg.Message;

        Conta? conta = await context.Contas
            .FirstOrDefaultAsync(c => c.UsuarioId == message.UsuarioId);

        if (conta is null)
        {
            await publishEndpoint.Publish(new PaymentRejectedEvent(
                usuarioId: message.UsuarioId,
                jogoId: message.JogoId,
                motivo: "Conta inexistente"
            ));

            return;
        }

        Transacao transacao = new(
            id: Guid.NewGuid(),
            usuarioId: message.UsuarioId,
            jogoId: message.JogoId,
            valor: message.Preco,
            status: EStatusOrdemCompra.Pendente,
            criadoEm: DateTime.UtcNow
        );
        
        context.Transacoes.Add(transacao);

        if (conta.Saldo >= message.Preco)
        {
            conta.Saldo -= message.Preco;
            transacao.Status = EStatusOrdemCompra.Aprovada;

            await context.SaveChangesAsync();

            await publishEndpoint.Publish(new PaymentApprovedEvent(
                usuarioId: message.UsuarioId,
                jogoId: message.JogoId,
                transacaoId: transacao.Id
            ));
        }
        else
        {
            transacao.Status = EStatusOrdemCompra.Rejeitada;

            await context.SaveChangesAsync();

            await publishEndpoint.Publish(new PaymentRejectedEvent(
                usuarioId: message.UsuarioId,
                jogoId: message.JogoId,
                motivo: "Saldo insuficiente"
            ));
        }
    }
}