using AutoMapper;
using Coffee.Application.Interfaces;
using Coffee.Application.Products.Queries.GetProductDetail;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDetailDto>
    {
        private readonly IRepository<Product> _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<ProductDetailDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);

            try
            {
                _repository.Update(product);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw new Exception("Продукт не найден");
            }

            ProductDetailDto productDetailDto = _mapper.Map<ProductDetailDto>(product);

            return productDetailDto;
        }
    }
}
