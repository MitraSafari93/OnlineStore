using Application;
using Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using OnlineStore.Domain;
using OnlineStore.Infrustructure;
using Xunit;

namespace TestProductService
{
    public class ProductServiceTest
    {


        [Fact]
        public async Task CreateProduct_WithValidData_CreatesProduct()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateProduct_WithValidData")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var productService = new ProductService(dbContext, memoryCache);

            var productRequest = new ProductRequestModel
            {
                Title = "Test Product",
                Count = 10,
                Price = 100m,
                Discount = 0.1m
            };

            await productService.CreateProduct(productRequest);
            var product = await dbContext.Products.FirstOrDefaultAsync();

            Assert.NotNull(product);
            Assert.Equal("Test Product", product.Title);
            Assert.Equal(10, product.InventoryCount);
            Assert.Equal(100m, product.OrginalPrice);
            Assert.Equal(0.1m, product.Discount);
            Assert.Equal(90m, product.DiscountedPrice);
        }

        [Fact]
        public async Task CreateProduct_WithDuplicateTitle_ThrowsException()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "CreateProduct_WithDuplicateTitle")
                .Options;
            var dbContext = new ApplicationDbContext(options);
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var productService = new ProductService(dbContext, memoryCache);

            var productRequest = new ProductRequestModel
            {
                Title = "Existing Product",
                Count = 10,
                Price = 100m,
                Discount = 0.1m
            };

            productService.CreateProduct(productRequest);

            var exception = await Assert.ThrowsAsync<Exception>(() => productService.CreateProduct(productRequest));
            Assert.Equal("Title must be unique", exception.Message);
        }
    }
}