using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProcessamentoArquivos.Domain.Entities;

namespace ProcessamentoArquivos.Infraestructure.Mapping
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Cpf)
                .IsRequired()
                .HasColumnType("varchar(11)");

            builder.HasIndex(c => c.Cpf)
                .IsUnique();

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(c => c.CreateAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.ToTable("Clientes");
        }
    }
}
