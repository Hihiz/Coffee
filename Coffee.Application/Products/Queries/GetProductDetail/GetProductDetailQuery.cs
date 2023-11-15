using MediatR;

namespace Coffee.Application.Products.Queries.GetProductDetail
{
    public class GetProductDetailQuery : IRequest<ProductDetailDto>
    {
        public int Id { get; set; }

        public GetProductDetailQuery(int id) => Id = id;
    }
}
