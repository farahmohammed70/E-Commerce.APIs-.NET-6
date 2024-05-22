using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Talabat.Core.Entities;
using Talabat.Core.Entities.Order_Aggregate;

namespace Talabat.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!context.ProductBrands.Any())
            {
                var BrandsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/brands.json");
                var Brands = JsonSerializer.Deserialize<List<ProductBrand>>(BrandsData);
                if(Brands is not null && Brands.Count > 0)
                {
                    foreach(var Brand in Brands)
                    {
                        await context.Set<ProductBrand>().AddAsync(Brand);
                        await context.SaveChangesAsync();
                    }
                }
            }

            if(!context.ProductTypes.Any())
            {
                var CatData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/types.json");
                var Categories = JsonSerializer.Deserialize<List<ProductType>>(CatData);
                if(Categories is not null && Categories.Count > 0)
                {
                    foreach(var Category in Categories)
                    {
                        await context.Set<ProductType>().AddAsync(Category);
                        await context.SaveChangesAsync();
                    }
                }
            }

            if(!context.Products.Any())
            {
                var ProductData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/products.json");
                var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                if(Products is not null && Products.Count > 0)
                {
                    foreach (var product in Products)
                    {
                        await context.Set<Product>().AddAsync(product);
                        await context.SaveChangesAsync();
                    }
                }
            }

            if (!context.DeliveryMethods.Any())
            {
                var DeliveryMethodsData = File.ReadAllText("../Talabat.Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(DeliveryMethodsData);
                if (deliveryMethods is not null && deliveryMethods.Count > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                    {
                        await context.Set<DeliveryMethod>().AddAsync(deliveryMethod);
                        await context.SaveChangesAsync();
                    }
                }
            }

        }
    }
}
