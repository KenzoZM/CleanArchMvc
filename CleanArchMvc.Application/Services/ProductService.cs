using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Domain.Entitites;
using CleanArchMvc.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _mapper = mapper;
        }

        public async Task<ProductDTO> GetProductCategoryAsync(int? id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productEntity = await _productRepository.GetProductsAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(productEntity);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int? id)
        {
            var productEntity = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(productEntity);
        }

        public async Task CreateProductAsync(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.CreateAsync(productEntity);
        }

        public async Task DeleteProductAsync(int? id)
        {
            var productEntity = _productRepository.GetByIdAsync(id).Result;
            await _productRepository.DeleteAsync(productEntity);
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            var productEntity = _mapper.Map<Product>(productDto);
            await _productRepository.UpdateAsync(productEntity);
        }
    }
}
