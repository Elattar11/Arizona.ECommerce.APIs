using Arizona.Core.Entities;
using Arizona.Core.Entities.OrderAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Arizona.Infrastructure.Data
{
    public static class StoreContextSeed
    {
        public async static Task SeedAsync(StoreContext _dbContext)
        {
            //start with brands and category 

            if (_dbContext.ProductBrands.Count() == 0) //if ProductBrands not contain any elements
            {
                var brandsData = File.ReadAllText("../Arizona.Repository/Data/DataSeeding/brands.json");

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                if (brands?.Count() > 0)
                {
                    //Projection to set ID to zero 
                    //brands = brands.Select(b => new ProductBrand()
                    //{ 
                    //    Name = b.Name
                    //}).ToList();

                    foreach (var brand in brands)
                    {
                        _dbContext.Set<ProductBrand>().Add(brand);
                    }
                    await _dbContext.SaveChangesAsync();
                } 
            }


            if (_dbContext.ProductCategories.Count() == 0) //if ProductBrands not contain any elements
            {
                var categoriesData = File.ReadAllText("../Arizona.Repository/Data/DataSeeding/categories.json");

                var categories = JsonSerializer.Deserialize<List<ProductCategory>>(categoriesData);

                if (categories?.Count() > 0)
                {
                    //Projection to set ID to zero 
                    //brands = brands.Select(b => new ProductBrand()
                    //{ 
                    //    Name = b.Name
                    //}).ToList();

                    foreach (var category in categories)
                    {
                        _dbContext.Set<ProductCategory>().Add(category);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }

            if (_dbContext.Products.Count() == 0) //if ProductBrands not contain any elements
            {
                var productsData = File.ReadAllText("../Arizona.Repository/Data/DataSeeding/products.json");

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                if (products?.Count() > 0)
                {
                    //Projection to set ID to zero 
                    //brands = brands.Select(b => new ProductBrand()
                    //{ 
                    //    Name = b.Name
                    //}).ToList();

                    foreach (var product in products)
                    {
                        _dbContext.Set<Product>().Add(product);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }


            if (_dbContext.DeliveryMethods.Count() == 0) //if ProductBrands not contain any elements
            {
                var deliveryMethodsData = File.ReadAllText("../Arizona.Repository/Data/DataSeeding/delivery.json");

                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);

                if (deliveryMethods?.Count() > 0)
                {
                    //Projection to set ID to zero 
                    //brands = brands.Select(b => new ProductBrand()
                    //{ 
                    //    Name = b.Name
                    //}).ToList();

                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        _dbContext.Set<DeliveryMethod>().Add(deliveryMethod);
                    }
                    await _dbContext.SaveChangesAsync();
                }
            }



        }
    }
}
