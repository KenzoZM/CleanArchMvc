using AutoMapper;
using CleanArchMvc.Application.DTOs;
using CleanArchMvc.Application.Interfaces;
using CleanArchMvc.Application.Products.Commands;
using CleanArchMvc.Application.Products.Queries;
using CleanArchMvc.Domain.Entitites;
using CleanArchMvc.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Application.Services
{
    public class ProductService : IProductService
    {
        private IMediator _mediator;
        private readonly IMapper _mapper;
        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        //public async Task<ProductDTO> GetProductCategoryAsync(int? id)
        //{
        //    var productByIdQuery = new GetProductByIdQuery(id.Value);

        //    if (productByIdQuery == null)
        //        throw new ApplicationException($"Entity could not be loaded");

        //    var result = await _mediator.Send(productByIdQuery);
        //    return _mapper.Map<ProductDTO>(result);
        //}

        public async Task<IEnumerable<ProductDTO>> GetProductsAsync()
        {
            var productsQuery = new GetProductsQuery();

            if (productsQuery == null)
            throw new ApplicationException($"Entity could not be loaded");

            var result = await _mediator.Send(productsQuery);
            return _mapper.Map<IEnumerable<ProductDTO>>(result);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int? id)
        {
            var productByIdQuery  = new GetProductByIdQuery(id.Value);

            if (productByIdQuery == null)
                throw new ApplicationException($"Entity could not be loaded");

            var result = await _mediator.Send(productByIdQuery);
            return _mapper.Map<ProductDTO>(result);

        }

        public async Task CreateProductAsync(ProductDTO productDto)
        {
            var createProductCommand = _mapper.Map<ProductCreateCommand>(productDto);
            await _mediator.Send(createProductCommand);
          
        }

        public async Task DeleteProductAsync(int? id)
        {
            var productRemoveCommand = new ProductRemoveCommand(id.Value);
            if (productRemoveCommand == null) throw new Exception($"Entity could not be loaded");
            await _mediator.Send(productRemoveCommand);
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            var UpdateProductCommand = _mapper.Map<ProductUpdateCommand>(productDto);
            await _mediator.Send(UpdateProductCommand);
        }
    }
}
