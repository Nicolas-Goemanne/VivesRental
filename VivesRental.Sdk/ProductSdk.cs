using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VivesRental.Services.Abstractions;
using VivesRental.Services.Model.Filters;
using VivesRental.Services.Model.Requests;
using VivesRental.Services.Model.Results;

namespace VivesRental.Sdk
{
    public class ProductSdk
    {
        private readonly IProductService _productService;

        public ProductSdk(IProductService productService)
        {
            _productService = productService;
        }

        public Task<ProductResult?> Get(Guid id)
        {
            return _productService.Get(id);
        }

        public Task<List<ProductResult>> Find(ProductFilter? filter)
        {
            return _productService.Find(filter);
        }

        public Task<ProductResult?> Create(ProductRequest entity)
        {
            return _productService.Create(entity);
        }

        public Task<ProductResult?> Edit(Guid id, ProductRequest entity)
        {
            return _productService.Edit(id, entity);
        }

        public Task<bool> Remove(Guid id)
        {
            return _productService.Remove(id);
        }

        public Task<bool> GenerateArticles(Guid productId, int amount)
        {
            return _productService.GenerateArticles(productId, amount);
        }
    }
}
