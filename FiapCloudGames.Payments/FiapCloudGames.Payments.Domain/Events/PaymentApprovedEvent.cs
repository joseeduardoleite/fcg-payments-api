using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Contracts.Events;

[ExcludeFromCodeCoverage]
public sealed class PaymentApprovedEvent
{
    public Guid UsuarioId { get; set; }
    public Guid JogoId { get; set; }
    public Guid TransacaoId { get; set; }

    public PaymentApprovedEvent() { }

    public PaymentApprovedEvent(Guid usuarioId, Guid jogoId, Guid transacaoId)
    {
        UsuarioId = usuarioId;
        JogoId = jogoId;
        TransacaoId = transacaoId;
    }
}