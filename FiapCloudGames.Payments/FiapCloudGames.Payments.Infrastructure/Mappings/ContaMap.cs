using FiapCloudGames.Payments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class ContaMap : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.HasKey(c => c.UsuarioId);

        builder.Property(c => c.UsuarioId)
            .IsRequired();

        builder.Property(c => c.Saldo)
            .IsRequired();
    }
}