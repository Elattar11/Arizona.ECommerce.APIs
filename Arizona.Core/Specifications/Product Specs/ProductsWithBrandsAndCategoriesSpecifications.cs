using Arizona.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arizona.Core.Specifications.Product_Specs
{
    public class ProductsWithBrandsAndCategoriesSpecifications : BaseSpecifications<Product>
    {
        public ProductsWithBrandsAndCategoriesSpecifications(ProductSpecParams specParams)
            :base(P => 
                    (string.IsNullOrEmpty(specParams.Search) || P.Name.ToLower().Contains(specParams.Search)) &&
                    (!specParams.BrandId.HasValue || P.BrandId == specParams.BrandId.Value) &&
                    (!specParams.CategoryId.HasValue || P.CategoryId == specParams.CategoryId.Value)
            )
        {
            SetIncludes();

            if (!string.IsNullOrEmpty(specParams.Sort))
            {
                switch (specParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(P => P.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(P => P.Price);
                        break;
                    default:
                        AddOrderBy(P => P.Name);
                        break;
                }
            }
            else
            {
                AddOrderBy(P => P.Name);
            }

            ApplyPagination((specParams.PageIndex - 1) * specParams.PageSize , specParams.PageSize);
        }

        public ProductsWithBrandsAndCategoriesSpecifications(int id)
            :base(P => P.Id == id)
        {
            SetIncludes();
        }

        private void SetIncludes()
        {
            Includes.Add(P => P.Brand);
            Includes.Add(P => P.Category);
        }
    }
}
