using CleanArchMvc.Domain.Entitites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Context
{// contexto de banco de dados // interação com o banco de dados
    public class ApplicationDbContext : DbContext
    { // definindo as opções de contexto
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        //Mapeando as entidades para as suas respectivas tabelas
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        //configurando o modelo do banco de dados usando a Fluent API
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // aplicando configurações nas entidades definidas no assembly applicationDbContext
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
