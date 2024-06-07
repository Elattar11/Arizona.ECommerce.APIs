using Arizona.Core;
using Arizona.Core.Entities;
using Arizona.Core.Services.Contract;
using Arizona.Core.Specifications.Product_Specs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Application.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IReadOnlyList<ProductBrand>> GetBrandsAsync()
            => await _unitOfWork.Repository<ProductBrand>().GetAllAsync();

        public async Task<IReadOnlyList<ProductCategory>> GetCategoriesAsync()
            => await _unitOfWork.Repository<ProductCategory>().GetAllAsync();

       

        public async Task<Product?> GetProductAsync(int productId)
        {
            var spec = new ProductsWithBrandsAndCategoriesSpecifications(productId);

            var product = await _unitOfWork.Repository<Product>().GetWithSpecAsync(spec);

            return product;
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams specParams)
        {
            var spec = new ProductsWithBrandsAndCategoriesSpecifications(specParams);

            var products = await _unitOfWork.Repository<Product>().GetAllWithSpecAsync(spec);

            return products;
        }
        public async Task<int> GetCountAsync(ProductSpecParams specParams)
        {
            var countSpec = new ProductsWithFilterForCountSpec(specParams);

            var count = await _unitOfWork.Repository<Product>().GetCountAsync(countSpec);

            return count;
        }
    }
}
