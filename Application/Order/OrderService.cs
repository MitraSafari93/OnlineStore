

using Application;
using OnlineStore.Domain;
using OnlineStore.Infrustructure;

namespace Application
{
    public class OrderService : IOrderService
    {
        public readonly IProductService _productService;
        public readonly IUserService _userService;
        private readonly ApplicationDbContext _dbContext;
        public OrderService(ApplicationDbContext dbContext, IProductService prodcutService, IUserService userService)
        {
            _dbContext = dbContext;
            _productService = prodcutService;
            _userService = userService;
        }

        public async Task CreateOrder(int userId, int productId)
        {
            var user = await _userService.GetUserById(userId);

            var product = await _productService.GetProductById(productId);

            if (product.InventoryCount < 1)
            {
                throw new Exception("The product is out of stock");
            }

            await _productService.UpdateInventoryCount(productId, product.InventoryCount-1);

            var order = new Order
            {
                Buyer = user,
                Product = product,
                CreationDate = DateTime.Now,
            };

            await  _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();
        }
    }
}
