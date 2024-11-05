using AutoMapper;
using Coffee.Application.Interfaces;
using Coffee.Application.Products.Queries.GetProductDetail;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ProductDetailDto>
    {
        private readonly IBaseRepository<Product> _repository;
        private readonly IMapper _mapper;

        public CreateProductCommandHandler(IBaseRepository<Product> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<ProductDetailDto> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);

            try
            {
                _repository.CreateAsync(product);
                await _repository.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            ProductDetailDto productDetailDto = _mapper.Map<ProductDetailDto>(product);

            return productDetailDto;
        }
    }
}
