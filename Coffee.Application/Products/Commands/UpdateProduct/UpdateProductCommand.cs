using Coffee.Application.Products.Queries.GetProductDetail;
using MediatR;

namespace Coffee.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ProductDetailDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string Image { get; set; }
        public int CategoryId { get; set; }
    }
}
