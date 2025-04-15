using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProdutosReactAPI.Dominio.Entidades;

namespace ProdutosReactAPI.Persistencia.Mapeamentos
{
    public class ProdutoMap : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");

            builder.HasKey(p => p.IdProduto);

            builder.Property(p => p.IdProduto)
                   .IsRequired()
                   .ValueGeneratedNever();

            builder.Property(p => p.Nome)
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(p => p.Valor)
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(p => p.DataInclusao)
                   .IsRequired();
        }
    }
}
