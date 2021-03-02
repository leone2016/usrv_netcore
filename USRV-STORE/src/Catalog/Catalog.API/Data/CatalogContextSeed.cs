using System.Collections.Generic;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            bool existProduct = productCollection.Find(p => true).Any();
            if (existProduct)
                productCollection.InsertManyAsync(GetPreconfigurations());
        }

        private static IEnumerable<Product> GetPreconfigurations()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Name = "Iphone Xr",
                    Summary = "designed in California",
                    ImageFile = "product-1.jpg",
                    Price = 650.10M,
                    Description = "Smart Phone"
                }, 
                new Product()
                {
                    Name = "Samsung 10",
                    Summary = "designed in China",
                    ImageFile = "product-1.jpg",
                    Price = 900.00M,
                    Description = "Smart Phone"
                },
                new Product()
                {
                    Name = "Huawei 20 Pro",
                    Summary = "designed in South Corea",
                    ImageFile = "product-1.jpg",
                    Price = 500.00M,
                    Description = "Smart Phone"
                }
            };
        }
    }
}