using CleanArchMvc.Domain.Entitites;
using CleanArchMvc.Domain.Interfaces;
using CleanArchMvc.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        // injenção de dependência
        private readonly ApplicationDbContext _productContext;

        public ProductRepository(ApplicationDbContext context)
        {
            _productContext = context;
        }

        public async Task<Product> CreateAsync(Product product)
        {
            // Adicione o produto ao DbSet de produtos no contexto do banco de dados
            _productContext.Products.Add(product);

            // Salve as mudanças no banco de dados
            await _productContext.SaveChangesAsync();

            return product; // Retorne o produto criado com o ID atualizado
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _productContext.Products.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> DeleteAsync(Product product)
        {
            _productContext.Products.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int? id)
        {
            //return await _productContext.Products.FindAsync(id);
            return await _productContext.Products.Include(c => c.Category).SingleOrDefaultAsync(p => p.Id == id);
        }

        //public async Task<Product> GetProductCategoryAsync(int? id)
        //{
        //    // incluindo a categoria relacionado ao produto cujo id foi passando como parametro
        //    //eager loading
        //    return await _productContext.Products.Include(p => p.Category).SingleOrDefaultAsync(p => p.Id == id);
        //}

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _productContext.Products.ToListAsync();
        }

    }
}
