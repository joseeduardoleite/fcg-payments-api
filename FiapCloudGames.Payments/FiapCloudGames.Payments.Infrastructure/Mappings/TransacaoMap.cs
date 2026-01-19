using FiapCloudGames.Payments.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace FiapCloudGames.Payments.Infrastructure.Mappings;

[ExcludeFromCodeCoverage]
public class TransacaoMap : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.HasKey(t => t.Id);

        builder.Property(c => c.UsuarioId)
            .IsRequired();

        builder.Property(c => c.JogoId)
            .IsRequired();

        builder.Property(c => c.Valor)
            .IsRequired();

        builder.Property(c => c.Status);

        builder.Property(c => c.CriadoEm)
            .IsRequired();
    }
}