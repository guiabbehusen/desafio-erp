using DesafioERP.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioERP.Data.Map
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(x => x.CPF);
            builder.Property(x => x.CPF).HasColumnType("char(11)").IsRequired();
            builder.Property(x => x.Nome).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(150);
            builder.Property(x => x.Telefone).HasMaxLength(20);
            builder.Property(x => x.Senha).IsRequired().HasMaxLength(100);

            builder.HasMany(x => x.Enderecos)
                   .WithOne()
                   .HasForeignKey(e => e.UsuarioCPF)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
