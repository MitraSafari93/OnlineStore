

namespace Application
{
    public interface IOrderService
    {
        public Task CreateOrder(int userId, int productId);
    }
}
