using Integration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using OnlineStore.Domain;
using OnlineStore.Infrustructure;

namespace Application
{
    public class ProductService : IProductService
    {
        private readonly  ApplicationDbContext _dbContext ;
        private readonly IMemoryCache _cache;
        private readonly MemoryCacheEntryOptions _cacheOptions;
        public ProductService(ApplicationDbContext dbContext, IMemoryCache memoryCache)
        {
            _dbContext = dbContext;
            _cache = memoryCache;
            _cacheOptions = new MemoryCacheEntryOptions()
                   .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        }

        public async Task CreateProduct(ProductRequestModel productRequestModel)
        {
            if (productRequestModel.Title.Length > 40)
                throw new ArgumentOutOfRangeException("Title must be less than 40 character.");

            if (await _dbContext.Products.AnyAsync(product => product.Title == productRequestModel.Title))
                throw new Exception("Title must be unique");


            var product = Product.Create(productRequestModel);

            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateInventoryCount(int productId, int inventoryCount)
        {
            var product = await _dbContext.Products.FindAsync(productId);
            if (product == null)
            {
                throw new Exception("Product not found.");
            }
            if (inventoryCount < 0)
            {
                throw new Exception($"{nameof(inventoryCount)} must be greater than zero.");
            }
            product.InventoryCount = inventoryCount;
            await _dbContext.SaveChangesAsync();
            _cache.Remove($"Product_{product.Id}");
        }

        public async Task<Product> GetProductById(int productId)
        {
            var key = $"Product_{productId}";

            if (!_cache.TryGetValue(key, out Product product))
            {
                product = await _dbContext.Products.FindAsync(productId);
                _cache.Set(key, product, _cacheOptions);
            }
            return product;
        }
    }
}