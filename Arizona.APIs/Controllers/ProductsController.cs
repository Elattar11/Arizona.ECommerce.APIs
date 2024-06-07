using Arizona.APIs.DTOs;
using Arizona.APIs.Errors;
using Arizona.APIs.Helpers;
using Arizona.Core.Entities;
using Arizona.Core.Repositories.Contract;
using Arizona.Core.Services.Contract;
using Arizona.Core.Specifications;
using Arizona.Core.Specifications.Product_Specs;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arizona.APIs.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(
            IProductService productService,
            IMapper mapper
            )
        {
            _productService = productService;
            _mapper = mapper;
        }


        //  /api/Products
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<Pagination<ProductToReturnDto>>> GetProducts([FromQuery] ProductSpecParams specParams)
        {

            var products = await _productService.GetProductsAsync(specParams);

            var count = await _productService.GetCountAsync(specParams);

            var data = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products);

            return Ok(new Pagination<ProductToReturnDto>(specParams.PageIndex , specParams.PageSize , count , data));

            //ok return object from OkObjectResult and status code 200ok 
        }


        //  /api/Products/1
        [ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>>GetProduct(int id)
        {
            
            var product = await _productService.GetProductAsync(id);

            if (product is null) return NotFound(new ApiResponse(404)); //status 404

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product)); //status 200
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
        {
            var brands = await _productService.GetBrandsAsync();
            return Ok(brands);
        }

        [HttpGet("categories")]

        public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategories()
        {
            var categories = await _productService.GetCategoriesAsync();
            return Ok(categories);
        }


    }
}
