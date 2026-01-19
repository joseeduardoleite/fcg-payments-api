using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Domain.Events;

[ExcludeFromCodeCoverage]
public class UserCreatedEvent
{
    public Guid UsuarioId { get; set; }
}