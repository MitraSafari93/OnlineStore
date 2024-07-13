

namespace Integration
{
    public class ProductRequestModel
    {
        public string Title { get; set; }
        public int Count { get; set; } = 1;
        public decimal? Discount { get; set; }
        public decimal? Price { get; set; }
    }
}
