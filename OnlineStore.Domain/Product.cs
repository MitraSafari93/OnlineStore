using Integration;
using System.Net.Http.Headers;

namespace OnlineStore.Domain
{
    public class Product
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required int InventoryCount { get; set; }
        public decimal Discount { get; set; } = default(decimal);
        public decimal OrginalPrice { get; set; } = default(decimal);
        public decimal DiscountedPrice { get { return OrginalPrice * (1 - Discount); } }

        private Product()
        {
            
        }

        public static Product Create(ProductRequestModel productReq)
        {
            var product = new Product()
            {
                InventoryCount = productReq.Count,
                Title = productReq.Title,
                OrginalPrice = productReq.Price??default,
                Discount = productReq.Discount??default,
            };
            if (IsValid(product)) 
            {
                return product;
            }
            return null;
        }

        private static bool IsValid(Product product)
        {
            if (product.Title == null)
                throw new InvalidDataException("Title must have value.");
            if (product.Title.Length > 40)
                throw new ArgumentOutOfRangeException("Title must be less than 40 character.");
            if (product.InventoryCount <= 0)
                throw new InvalidDataException("Inventory count must be greater than zero.");
            return true;

        }
    }

}