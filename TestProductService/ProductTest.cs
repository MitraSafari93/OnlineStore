using Integration;
using OnlineStore.Domain;
using Xunit;

namespace OnlineStore.Test
{
    public class ProductTest
    {
        [Fact]
        public void Create_WithValidData_ReturnsProduct()
        {
            var productRequest = new ProductRequestModel
            {
                Title = "Test Product",
                Count = 10,
                Price = 100m,
                Discount = 0.1m
            };

            var product = Product.Create(productRequest);

            Assert.NotNull(product);
            Assert.Equal("Test Product", product.Title);
            Assert.Equal(10, product.InventoryCount);
            Assert.Equal(100m, product.OrginalPrice);
            Assert.Equal(0.1m, product.Discount);
            Assert.Equal(90m, product.DiscountedPrice);
        }

        [Fact]
        public void Create_WithInvalidTitle_ThrowsException()
        {
            var productRequest = new ProductRequestModel
            {
                Title = null,
                Count = 10,
                Price = 100m,
                Discount = 0.1m
            };

            var exception = Assert.Throws<InvalidDataException>(() => Product.Create(productRequest));
            Assert.Equal("Title must have value.", exception.Message);
        }
    }
}

