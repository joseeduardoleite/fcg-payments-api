using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Contracts.Events;

[ExcludeFromCodeCoverage]
public sealed class PaymentRejectedEvent
{
    public Guid UsuarioId { get; set; }
    public Guid JogoId { get; set; }
    public string Motivo { get; set; } = string.Empty;

    public PaymentRejectedEvent() { }

    public PaymentRejectedEvent(Guid usuarioId, Guid jogoId, string motivo)
    {
        UsuarioId = usuarioId;
        JogoId = jogoId;
        Motivo = motivo;
    }
}