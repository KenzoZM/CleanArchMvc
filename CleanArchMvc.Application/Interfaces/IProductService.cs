using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProductsAsync();
        Task<ProductDTO> GetProductByIdAsync(int? id);
        Task<ProductDTO> GetProductCategoryAsync(int? id);

        Task CreateProductAsync(ProductDTO productDto);
        Task UpdateProductAsync(ProductDTO productDto);
        Task DeleteProductAsync(int? id);
    }
}
