using FiapCloudGames.Payments.Domain.Enums;

namespace FiapCloudGames.Payments.Domain.Entities;

public class Transacao
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid JogoId { get; set; }
    public decimal Valor { get; set; }
    public EStatusOrdemCompra? Status { get; set; }
    public DateTime CriadoEm { get; set; }

    public Transacao() { }

    public Transacao(
        Guid id,
        Guid usuarioId,
        Guid jogoId,
        decimal valor,
        EStatusOrdemCompra? status,
        DateTime criadoEm)
    {
        Id = id;
        UsuarioId = usuarioId;
        JogoId = jogoId;
        Valor = valor;
        Status = status;
        CriadoEm = criadoEm;
    }
}