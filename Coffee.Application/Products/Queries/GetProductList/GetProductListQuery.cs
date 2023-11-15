using MediatR;

namespace Coffee.Application.Products.Queries.GetProductList
{
    public class GetProductListQuery : IRequest<ProductListViewModel>
    {
        public int Id { get; set; }
    }
}
