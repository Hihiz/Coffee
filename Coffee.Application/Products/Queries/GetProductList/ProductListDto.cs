namespace Coffee.Application.Products.Queries.GetProductList
{
    public class ProductListDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

    }
}
