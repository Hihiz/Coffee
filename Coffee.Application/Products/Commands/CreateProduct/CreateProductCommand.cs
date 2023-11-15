using Coffee.Application.Products.Queries.GetProductDetail;
using MediatR;

namespace Coffee.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<ProductDetailDto>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public string? Image { get; set; }
        public int CategoryId { get; set; }
    }
}
