using DesafioERP.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class EnderecoMap : IEntityTypeConfiguration<Endereco>
{
    public void Configure(EntityTypeBuilder<Endereco> builder)
    {
        builder.ToTable("Enderecos");

        builder.HasKey(e => e.Id);

        builder.Property(e => e.CEP)
               .IsRequired()
               .HasMaxLength(9);

        builder.Property(e => e.Rua)
               .IsRequired()
               .HasMaxLength(100);

        builder.Property(e => e.Numero)
               .IsRequired()
               .HasMaxLength(10);

        builder.Property(e => e.Bairro)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.Cidade)
               .IsRequired()
               .HasMaxLength(50);

        builder.Property(e => e.Estado)
               .IsRequired()
               .HasMaxLength(2);

        builder.HasOne(e => e.Usuario)
               .WithMany(u => u.Enderecos)
               .HasForeignKey(e => e.UsuarioCPF)
               .OnDelete(DeleteBehavior.Cascade);
    }
}
