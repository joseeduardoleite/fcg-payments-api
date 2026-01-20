using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Contracts.Events;

[ExcludeFromCodeCoverage]
public class UserCreatedEvent
{
    public Guid UsuarioId { get; init; }
    public string? Nome { get; set; }
}