using CleanArchMvc.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // reforçando que a propriedade Id vai ser a chave prim
            builder.HasKey(p => p.Id);

            // configurando a propriedade name com no maximo 100 caracteres é o tornando obrigatorio não aceitando valores nulos
            builder.Property(p => p.Name).HasMaxLength(100).IsRequired();

            // configurando a propriedade description com no maximo 200 caracteres é o tornando obrigatorio não aceitando valores nulos
            builder.Property(p => p.Description).HasMaxLength(200).IsRequired();

            // configurando a propriedade Price com uma precisão de 10 com 2 casas decimais.
            builder.Property(p => p.Price).HasPrecision(10,2).IsRequired();

            // configurando o relacionamento de 1 para muitos entre categoria e produtos e reforçando que CategoryId é a chave estrangeira
            builder.HasOne(e => e.Category).WithMany(e => e.Products).HasForeignKey(e => e.CategoryId);
        }
    }
}
