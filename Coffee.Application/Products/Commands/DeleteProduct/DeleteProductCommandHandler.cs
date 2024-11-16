using Coffee.Application.Common.Exceptions;
using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, int>
    {
        private readonly IBaseRepository<Product> _repository;

        public DeleteProductCommandHandler(IBaseRepository<Product> repository) => (_repository) = (repository);

        public async Task<int> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            Product product = await _repository.GetByIdAsync(request.Id) ??
              throw new NotFoundException("Продукт не найден", request.Id);

            try
            {
                await _repository.Delete(product);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

            return product.Id;
        }
    }
}
