using MediatR;

namespace Coffee.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<int>
    {
        public int Id { get; set; }

        public DeleteProductCommand(int id) => Id = id;
    }
}
