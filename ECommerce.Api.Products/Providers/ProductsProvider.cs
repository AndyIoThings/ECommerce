using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ECommerce.Api.Products.Interfaces;
using ECommerce.Api.Products.Db;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Api.Products.Providers
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger<ProductsProvider> logger;
        private readonly IMapper mapper;
        public ProductsProvider(ProductsDbContext dbContext,
       ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;
            SeedData();

        }
        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 1,
                    Name = "Keyboard",
                    Price = 20,
                    Inventory = 100
                });
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 2,
                    Name = "Mouse",
                    Price = 5,
                    Inventory = 200
                });
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 3,
                    Name = "Monitor",
                    Price = 150,
                    Inventory = 1000
                });
                dbContext.Products.Add(new Db.Product()
                {
                    Id = 4,
                    Name = "CPU",
                    Price = 200,
                    Inventory = 2000
                });
                dbContext.SaveChanges();
            }
        }
        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()

        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var result = mapper.Map<IEnumerable<ECommerce.Api.Products.Db.Product>,
                    IEnumerable<ECommerce.Api.Products.Models.Product>>(products);
                    return (true, result, null);
                }
                return (false, null, "Not found");
            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

    }
}
