using AutoMapper;
using Coffee.Application.Common.Exceptions;
using Coffee.Application.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Products.Queries.GetProductDetail
{
    public class GetProductDetailQueryHandler : IRequestHandler<GetProductDetailQuery, ProductDetailDto>
    {
        private readonly IBaseRepository<Product> _repository;
        private readonly IMapper _mapper;

        public GetProductDetailQueryHandler(IBaseRepository<Product> repository, IMapper mapper) => (_repository, _mapper) = (repository, mapper);

        public async Task<ProductDetailDto> Handle(GetProductDetailQuery request, CancellationToken cancellationToken)
        {
            Product product = await _repository.GetByIdAsync(request.Id) ??
                throw new NotFoundException(nameof(Product), request.Id);

            return _mapper.Map<ProductDetailDto>(product);
        }
    }
}
