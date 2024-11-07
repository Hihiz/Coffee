using AutoMapper;
using Coffee.Application.Interfaces;
using Coffee.Application.Products.Queries.GetProductDetail;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ProductDetailDto>
    {
        private readonly IBaseRepository<Product> _repository;
        private readonly IMapper _mapper;

        public UpdateProductCommandHandler(IBaseRepository<Product> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<ProductDetailDto> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            Product product = _mapper.Map<Product>(request);
            ProductDetailDto productDetailDto = new ProductDetailDto();

            try
            {
                product = await _repository.UpdateAsync(product);
                await _repository.SaveChangesAsync();

                productDetailDto = _mapper.Map<ProductDetailDto>(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }

            return productDetailDto;
        }
    }
}
