using Integration;
using OnlineStore.Domain;

namespace Application
{
    public interface IProductService
    {
        public Task CreateProduct(ProductRequestModel prodcut);
        public Task UpdateInventoryCount(int productId, int count);
        public Task<Product> GetProductById(int productId);
    }
}