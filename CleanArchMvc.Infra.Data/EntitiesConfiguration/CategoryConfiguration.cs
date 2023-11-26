using CleanArchMvc.Domain.Entities;
using CleanArchMvc.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// configurando as entidades para mapeamento no banco de dados
namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            // reforçando que a propriedade Id vai ser a chave primaria
            builder.HasKey(c => c.Id);

            // configurando a propriedade name com no maximo 100 caracteres é o tornando obrigatorio não aceitando valores nulos
            builder.Property(c => c.Name).HasMaxLength(100).IsRequired();

            // Populando o banco de dados
            builder.HasData(
                new Category(1, "Material Escolar"),
                new Category(2, "Eletrônicos"),
                new Category(3, "Acessórios")
            );
        }
    }
}
