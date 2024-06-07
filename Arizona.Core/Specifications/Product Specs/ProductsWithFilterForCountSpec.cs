using Arizona.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core.Specifications.Product_Specs
{
    public class ProductsWithFilterForCountSpec : BaseSpecifications<Product>
    {
        public ProductsWithFilterForCountSpec(ProductSpecParams specParams)
            : base(P =>
            (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
                    (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value) && 
                    (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value)
            )
        {
            
        }
    }
}
