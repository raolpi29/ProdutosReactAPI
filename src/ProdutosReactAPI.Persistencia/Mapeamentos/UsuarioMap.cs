using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Persistencia.Mapeamentos
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuarios");

            builder.HasKey(u => u.IdUsuario);

            builder.Property(u => u.IdUsuario)
                   .IsRequired()
                   .ValueGeneratedNever();

            builder.Property(u => u.Login)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(u => u.Senha)
                   .IsRequired();

            builder.Property(u => u.DataInclusao)
                   .IsRequired();
        }
    }
}
