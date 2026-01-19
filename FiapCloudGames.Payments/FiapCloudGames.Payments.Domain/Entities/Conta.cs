namespace FiapCloudGames.Payments.Domain.Entities;

public class Conta
{
    public Guid UsuarioId { get; set; }
    public decimal Saldo { get; set; }

    public bool Debitar(decimal valor)
    {
        if (Saldo < valor)
            return false;

        Saldo -= valor;
        return true;
    }
}