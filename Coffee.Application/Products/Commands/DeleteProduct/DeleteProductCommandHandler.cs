using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IRepository<Product> _repository;

        public DeleteProductCommandHandler(IRepository<Product> repository) => (_repository) = (repository);

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _repository.GetById(request.Id);

            if (product == null)
            {
                throw new Exception("Продукт не найден");
            }

            try
            {
                _repository.Delete(product);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Продукт не найден");
            }

            return product.Id;
        }
    }
}
